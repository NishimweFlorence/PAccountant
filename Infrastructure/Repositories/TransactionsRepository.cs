using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Application.DTO;

namespace Infrastructure.Repositories
{
    public class TransactionsRepository : ITransaction
    {
        private readonly ApplicationDbContext _context;

        public TransactionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Transaction GetTransactionById(int Id)
        {
            return _context.Transactions.Find(Id);
        }

        public List<Transaction> GetAllTransactions()
        {
            return _context.Transactions.ToList();
        }

        public void CreateTransaction(CreateTransactionDTO TransactionDTO)
        {
            var transaction = new Transaction
            {
                IdTransactionCategory = TransactionDTO.IdTransactionCategory,
                TransactionDate       = TransactionDTO.TransactionDate,
                Amount                = TransactionDTO.Amount,
                Currency              = TransactionDTO.Currency,
                AccountId             = TransactionDTO.AccountId,
                CreatedAt             = TransactionDTO.CreatedAt,
            };
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public void UpdateTransaction(int Id, UpdateTransactionDTO TransactionDTO)
        {
            var transaction = _context.Transactions.Find(Id);
            if (transaction != null)
            {
                transaction.IdTransactionCategory = TransactionDTO.IdTransactionCategory;
                transaction.TransactionDate        = TransactionDTO.TransactionDate;  // was missing
                transaction.Amount                 = TransactionDTO.Amount;
                transaction.Currency               = TransactionDTO.Currency;
                transaction.AccountId              = TransactionDTO.AccountId;
                _context.SaveChanges();
            }
        }
    }
}