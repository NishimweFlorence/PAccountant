using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Application.DTO;
using Microsoft.EntityFrameworkCore;
using Domain.ValueObjects;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class TransactionsRepository : ITransaction
    {
        private readonly ApplicationDbContext _context;

        public TransactionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int Id)
        {
            return await _context.Transactions
                .Include(t => t.Entries)
                    .ThenInclude(e => e.Account)
                .Include(t => t.Entries)
                    .ThenInclude(e => e.Category)
                .Include(t => t.Budget)
                .FirstOrDefaultAsync(t => t.Id == Id);
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions
                .Include(t => t.Entries)
                    .ThenInclude(e => e.Account)
                .Include(t => t.Entries)
                    .ThenInclude(e => e.Category)
                .Include(t => t.Budget)
                .ToListAsync();
        }

        public async Task CreateTransactionAsync(CreateTransactionDTO TransactionDTO)
        {
            var transaction = new Transaction
            {
                TransactionDate       = TransactionDTO.TransactionDate,
                Description           = TransactionDTO.Description,
                ReferenceNumber       = TransactionDTO.ReferenceNumber,
                BudgetId              = TransactionDTO.BudgetId,
                Currency              = Currency.FromCode(TransactionDTO.Currency),
                CreatedAt             = TransactionDTO.CreatedAt,
                Entries               = TransactionDTO.Entries.Select(e => new TransactionEntry
                {
                    AccountId = e.AccountId,
                    CategoryId = e.CategoryId,
                    Debit = e.Debit,
                    Credit = e.Credit
                }).ToList()
            };
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransactionAsync(int Id, UpdateTransactionDTO TransactionDTO)
        {
            var transaction = await _context.Transactions
                .Include(t => t.Entries)
                .FirstOrDefaultAsync(t => t.Id == Id);

            if (transaction != null)
            {
                transaction.TransactionDate        = TransactionDTO.TransactionDate;
                transaction.Description            = TransactionDTO.Description;
                transaction.ReferenceNumber        = TransactionDTO.ReferenceNumber;
                transaction.BudgetId               = TransactionDTO.BudgetId;
                transaction.Currency               = Currency.FromCode(TransactionDTO.Currency);

                _context.TransactionEntries.RemoveRange(transaction.Entries);
                transaction.Entries = TransactionDTO.Entries.Select(e => new TransactionEntry
                {
                    AccountId = e.AccountId,
                    CategoryId = e.CategoryId,
                    Debit = e.Debit,
                    Credit = e.Credit,
                    TransactionId = Id
                }).ToList();

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }
    }
}
