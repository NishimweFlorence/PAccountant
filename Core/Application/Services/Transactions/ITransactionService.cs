using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITransactionService
    {
        List<Transaction> GetAllTransactions();
        Transaction GetTransactionById(int id);
        void CreateTransaction(CreateTransactionDTO TransactionDTO);
        void UpdateTransaction(int Id, UpdateTransactionDTO TransactionDTO);
    }
}