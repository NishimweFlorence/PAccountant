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
    public class ExpenseRepository : IExpense
    {
        private readonly ApplicationDbContext _context;

        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ExpenseReadDTO> CreateExpenseAsync(ExpenseCreateDTO expenseDto)
        {
            var expense = new Expense
            {
                Amount = expenseDto.Amount,
                Date = expenseDto.Date,
                Description = expenseDto.Description,
                Type = ExpenseType.FromString(expenseDto.Type),
                AccountId = expenseDto.AccountId,
                AssetId = expenseDto.AssetId,
                LiabilityId = expenseDto.LiabilityId,
                Currency = Currency.FromCode(expenseDto.Currency),
                CreatedAt = DateTime.Now
            };

            await _context.Expenses.AddAsync(expense);

            // Create Transaction
            var category = await GetOrCreateExpenseCategory(expenseDto.Type);

            var transaction = new Transaction
            {
                TransactionDate = expenseDto.Date,
                Description = $"Expense: {expenseDto.Description} ({expenseDto.Type})",
                Currency = Currency.FromCode(expenseDto.Currency),
                CreatedAt = DateTime.Now,
                Entries = new List<TransactionEntry>()
            };

            // Dr Expense Category
            transaction.Entries.Add(new TransactionEntry { CategoryId = category.Id, Debit = expenseDto.Amount, Credit = 0 });

            // Cr Account, Asset, or Liability
            if (expenseDto.AccountId.HasValue)
            {
                transaction.Entries.Add(new TransactionEntry { AccountId = expenseDto.AccountId.Value, Debit = 0, Credit = expenseDto.Amount });
                
                var account = await _context.Accounts.FindAsync(expenseDto.AccountId.Value);
                if (account != null)
                {
                    account.Balance = (account.Balance ?? 0) - expenseDto.Amount;
                }
            }
            else if (expenseDto.LiabilityId.HasValue)
            {
                var liability = await _context.Liabilities.FindAsync(expenseDto.LiabilityId.Value);
                if (liability != null)
                {
                    liability.CurrentAmount -= expenseDto.Amount;
                }
            }

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            expense.TransactionId = transaction.Id;
            await _context.SaveChangesAsync();

            return MapToReadDTO(expense);
        }

        public async Task<List<ExpenseReadDTO>> GetAllExpensesAsync()
        {
            var expenses = await _context.Expenses
                .Include(e => e.Account)
                .Include(e => e.Asset)
                .Include(e => e.Liability)
                .ToListAsync();
            return expenses.Select(MapToReadDTO).ToList();
        }

        public async Task<ExpenseReadDTO?> GetExpenseByIdAsync(int id)
        {
            var expense = await _context.Expenses
                .Include(e => e.Account)
                .Include(e => e.Asset)
                .Include(e => e.Liability)
                .FirstOrDefaultAsync(e => e.Id == id);
            
            return expense == null ? null : MapToReadDTO(expense);
        }

        private async Task<TransactionCategory> GetOrCreateExpenseCategory(string typeName)
        {
            var category = await _context.TransactionCategories.FirstOrDefaultAsync(c => c.Name == typeName);
            if (category == null)
            {
                category = new TransactionCategory 
                { 
                    Name = typeName, 
                    Type = TransactionType.Expense
                };
                await _context.TransactionCategories.AddAsync(category);
                await _context.SaveChangesAsync();
            }
            return category;
        }

        private ExpenseReadDTO MapToReadDTO(Expense expense)
        {
            return new ExpenseReadDTO
            {
                Id = expense.Id,
                Amount = expense.Amount,
                Date = expense.Date,
                Description = expense.Description,
                Type = expense.Type.Name,
                AccountId = expense.AccountId,
                AccountName = expense.Account?.Name,
                AssetId = expense.AssetId,
                AssetName = expense.Asset?.Name,
                LiabilityId = expense.LiabilityId,
                LiabilityName = expense.Liability?.LenderName,
                Currency = expense.Currency.Code,
                TransactionId = expense.TransactionId
            };
        }
    }
}
