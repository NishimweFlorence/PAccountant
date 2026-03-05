using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task CreateTransactionAsync(CreateTransactionDTO TransactionDTO);
        Task UpdateTransactionAsync(int Id, UpdateTransactionDTO TransactionDTO);
        Task DeleteTransactionAsync(int id);
    }
}
