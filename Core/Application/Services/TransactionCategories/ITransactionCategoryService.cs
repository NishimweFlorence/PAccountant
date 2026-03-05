using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITransactionCategoryService
    {
        Task<List<TransactionCategory>> GetAllTransactionCategoriesAsync();
        Task<TransactionCategory?> GetTransactionCategoryByIdAsync(int id);
        Task CreateTransactionCategoryAsync(TransactionCategoryCreateDTO transactionCategoryCreateDTO);
        Task UpdateTransactionCategoryAsync(int id, TransactionCategoryUpdateDTO transactionCategoryUpdateDTO);
    }
}
