using Application.DTO;
using Application.Interfaces;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface ITransactionCategory
    {
        Task<List<TransactionCategory>> GetAllTransactionCategoriesAsync();
        Task<TransactionCategory?> GetTransactionCategoryByIdAsync(int id);
        Task CreateTransactionCategoryAsync(TransactionCategoryCreateDTO transactionCategoryCreateDTO);
        Task UpdateTransactionCategoryAsync(int id, TransactionCategoryUpdateDTO transactionCategoryUpdateDTO);
    }
}