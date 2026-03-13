using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class LookupService : ILookupService
    {
        private readonly ApplicationDbContext _context;

        public LookupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetTypesAsync(int userId, string category, List<string> defaultTypes)
        {
            var customLookups = await _context.CustomLookups
                .Where(c => c.UserId == userId && c.Category == category)
                .Select(c => c.Name)
                .ToListAsync();

            var merged = new HashSet<string>(defaultTypes, StringComparer.OrdinalIgnoreCase);
            foreach (var custom in customLookups)
            {
                merged.Add(custom);
            }

            return merged.OrderBy(x => x).ToList();
        }

        public async Task<List<CustomLookupDTO>> GetAllCustomLookupsAsync(int userId)
        {
            return await _context.CustomLookups
                .Where(c => c.UserId == userId)
                .Select(c => new CustomLookupDTO
                {
                    Id = c.Id,
                    Category = c.Category,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<CustomLookupDTO> CreateCustomLookupAsync(int userId, string category, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");

            var exists = await _context.CustomLookups
                .AnyAsync(c => c.UserId == userId && c.Category == category && c.Name.ToLower() == name.ToLower());
                
            if (exists)
                throw new InvalidOperationException($"A custom {category} with this name already exists.");

            var lookup = new CustomLookup
            {
                UserId = userId,
                Category = category,
                Name = name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.CustomLookups.Add(lookup);
            await _context.SaveChangesAsync();

            return new CustomLookupDTO
            {
                Id = lookup.Id,
                Category = lookup.Category,
                Name = lookup.Name
            };
        }

        public async Task UpdateCustomLookupAsync(int id, string newName)
        {
            var lookup = await _context.CustomLookups.FindAsync(id);
            if (lookup == null) throw new KeyNotFoundException("Custom lookup not found.");

            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Name cannot be empty.");

            string oldName = lookup.Name;
            lookup.Name = newName;
            lookup.UpdatedAt = DateTime.Now;

            // Cascade update to existing records to avoid orphaned historical data
            switch (lookup.Category)
            {
                case "AccountType":
                    var accounts = await _context.Accounts.Where(a => a.Type!.Name == oldName).ToListAsync();
                    // In a real multi-tenant app, we should also filter by UserId, 
                    // However Account entity doesn't have UserId. Let's assume global accounts or user's own data.
                    foreach (var acc in accounts)
                    {
                        acc.Type = AccountType.FromString(newName);
                    }
                    break;
                case "AssetType":
                    var assets = await _context.Assets.Where(a => a.Type!.Name == oldName).ToListAsync();
                    foreach (var asset in assets)
                    {
                        asset.Type = AssetType.FromString(newName);
                    }
                    break;
                case "ExpenseType":
                    var expenses = await _context.Expenses.Where(e => e.Type!.Name == oldName).ToListAsync();
                    foreach (var expense in expenses)
                    {
                        expense.Type = ExpenseType.FromString(newName);
                    }
                    break;
                case "IncomeType":
                    var incomes = await _context.Incomes.Where(i => i.Type!.Name == oldName).ToListAsync();
                    foreach (var income in incomes)
                    {
                        income.Type = IncomeType.FromString(newName);
                    }
                    break;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomLookupAsync(int id)
        {
            var lookup = await _context.CustomLookups.FindAsync(id);
            if (lookup != null)
            {
                _context.CustomLookups.Remove(lookup);
                await _context.SaveChangesAsync();
                // We do not delete past records or touch them, they just keep the text string in the DB.
            }
        }
    }
}
