using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class IncomeRepository : IIncome
    {
        private readonly ApplicationDbContext _context;

        public IncomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IncomeReadDTO> CreateIncomeAsync(IncomeCreateDTO incomeDto)
        {
            var income = new Income
            {
                Amount = incomeDto.Amount,
                Date = incomeDto.Date,
                Description = incomeDto.Description,
                Type = IncomeType.FromString(incomeDto.Type),
                AccountId = incomeDto.AccountId,
                AssetId = incomeDto.AssetId,
                Currency = Currency.FromCode(incomeDto.Currency),
                CreatedAt = DateTime.Now
            };

            await _context.Incomes.AddAsync(income);

            // Create Transaction
            var category = await GetOrCreateIncomeCategory(incomeDto.Type);
            
            var transaction = new Transaction
            {
                TransactionDate = incomeDto.Date,
                Description = $"Income: {incomeDto.Description} ({incomeDto.Type})",
                Currency = Currency.FromCode(incomeDto.Currency),
                CreatedAt = DateTime.Now,
                Entries = new List<TransactionEntry>()
            };

            // Dr Account or Asset
            if (incomeDto.AccountId.HasValue)
            {
                transaction.Entries.Add(new TransactionEntry { AccountId = incomeDto.AccountId.Value, Debit = incomeDto.Amount, Credit = 0 });
                
                var account = await _context.Accounts.FindAsync(incomeDto.AccountId.Value);
                if (account != null)
                {
                    account.Balance = (account.Balance ?? 0) + incomeDto.Amount;
                }
            }
            else if (incomeDto.AssetId.HasValue)
            {
                // Simple logic for asset: increase current value? 
                // Usually income increases cash/bank, but if it increases asset value directly:
                var asset = await _context.Assets.FindAsync(incomeDto.AssetId.Value);
                if (asset != null)
                {
                    asset.CurrentValue += incomeDto.Amount;
                }
                // We might need an 'Asset' account or a way to track this in entries. 
                // For now, if no account, we use the asset entry (assuming it behaves like an account).
                // Actually, TransactionEntry usually points to an Account.
            }

            // Cr Income Category
            transaction.Entries.Add(new TransactionEntry { CategoryId = category.Id, Debit = 0, Credit = incomeDto.Amount });

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            
            income.TransactionId = transaction.Id;
            await _context.SaveChangesAsync();

            return MapToReadDTO(income);
        }

        public async Task<List<IncomeReadDTO>> GetAllIncomesAsync()
        {
            var incomes = await _context.Incomes
                .Include(i => i.Account)
                .Include(i => i.Asset)
                .ToListAsync();
            return incomes.Select(MapToReadDTO).ToList();
        }

        public async Task<IncomeReadDTO?> GetIncomeByIdAsync(int id)
        {
            var income = await _context.Incomes
                .Include(i => i.Account)
                .Include(i => i.Asset)
                .FirstOrDefaultAsync(i => i.Id == id);
            
            return income == null ? null : MapToReadDTO(income);
        }

        private async Task<TransactionCategory> GetOrCreateIncomeCategory(string typeName)
        {
            var category = await _context.TransactionCategories.FirstOrDefaultAsync(c => c.Name == typeName);
            if (category == null)
            {
                category = new TransactionCategory 
                { 
                    Name = typeName, 
                    Type = TransactionType.Income 
                };
                await _context.TransactionCategories.AddAsync(category);
                await _context.SaveChangesAsync();
            }
            return category;
        }

        private IncomeReadDTO MapToReadDTO(Income income)
        {
            return new IncomeReadDTO
            {
                Id = income.Id,
                Amount = income.Amount,
                Date = income.Date,
                Description = income.Description,
                Type = income.Type.Name,
                AccountId = income.AccountId,
                AccountName = income.Account?.Name,
                AssetId = income.AssetId,
                AssetName = income.Asset?.Name,
                Currency = income.Currency.Code,
                TransactionId = income.TransactionId
            };
        }
    }
}
