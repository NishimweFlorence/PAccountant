using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Application.DTO;
using Microsoft.EntityFrameworkCore;
using Domain.ValueObjects;

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
                .Include(t => t.Category)
                .Include(t => t.Account)
                .FirstOrDefaultAsync(t => t.Id == Id);
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .Include(t => t.Account)
                .ToListAsync();
        }

        public async Task CreateTransactionAsync(CreateTransactionDTO TransactionDTO)
        {
            var transaction = new Transaction
            {
                IdTransactionCategory = TransactionDTO.IdTransactionCategory,
                TransactionDate       = TransactionDTO.TransactionDate,
                Amount                = TransactionDTO.Amount,
                Currency              = Currency.FromCode(TransactionDTO.Currency),
                AccountId             = TransactionDTO.AccountId,
                CreatedAt             = TransactionDTO.CreatedAt,
            };
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransactionAsync(int Id, UpdateTransactionDTO TransactionDTO)
        {
            var transaction = await _context.Transactions.FindAsync(Id);
            if (transaction != null)
            {
                transaction.IdTransactionCategory = TransactionDTO.IdTransactionCategory;
                transaction.TransactionDate        = TransactionDTO.TransactionDate;
                transaction.Amount                 = TransactionDTO.Amount;
                transaction.Currency               = Currency.FromCode(TransactionDTO.Currency);
                transaction.AccountId              = TransactionDTO.AccountId;
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
