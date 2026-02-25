using Domain.Entities;
using Application.DTO;
using Application.Interfaces;

namespace Application.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransaction _Transaction;
        public TransactionService(ITransaction Transaction)
        {
            _Transaction = Transaction;
        }
        public List<Transaction> GetAllTransactions()
        {
            List<Transaction> _Transactions = _Transaction.GetAllTransactions();
            return _Transactions;
        }

        public Transaction GetTransactionById(int id)
        {
            return _Transaction.GetTransactionById(id);
        }
        public void CreateTransaction(CreateTransactionDTO TransactionDTO)
        {
            _Transaction.CreateTransaction(TransactionDTO);
        }
        public void UpdateTransaction(int Id, UpdateTransactionDTO TransactionDTO)
        {
            _Transaction.UpdateTransaction(Id, TransactionDTO);
        }
    }
}