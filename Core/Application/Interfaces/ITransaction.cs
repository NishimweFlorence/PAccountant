using System.Collections.Generic;
using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITransaction
    {
        Task<Transaction> GetTransactionByIdAsync(int Id);
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task CreateTransactionAsync(CreateTransactionDTO TransactionDTO);
        Task UpdateTransactionAsync(int Id, UpdateTransactionDTO TransactionDTO);
        Task DeleteTransactionAsync(int id);
    }
}
