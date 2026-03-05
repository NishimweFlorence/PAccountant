using System.Security.Cryptography.X509Certificates;
using Application.Interfaces;
using Domain.Entities;
using Application.DTO;


namespace Application.Services.TransactionCategories
{
    
    public class TransactionCategoryService : ITransactionCategoryService
    {
        private readonly ITransactionCategory _transactionCategory;

        //Constructor
        public TransactionCategoryService(ITransactionCategory transactionCategory)
        {
            _transactionCategory = transactionCategory;
        }
        
        public async Task<List<TransactionCategory>> GetAllTransactionCategoriesAsync()
        {
            return await _transactionCategory.GetAllTransactionCategoriesAsync();
        }

        public async Task<TransactionCategory?> GetTransactionCategoryByIdAsync(int id)
        {
            return await _transactionCategory.GetTransactionCategoryByIdAsync(id);
        }   

        public async Task CreateTransactionCategoryAsync(TransactionCategoryCreateDTO transactionCategoryDTO)
        {  
            await _transactionCategory.CreateTransactionCategoryAsync(transactionCategoryDTO);
        }

        public async Task UpdateTransactionCategoryAsync(int id, TransactionCategoryUpdateDTO transactionCategoryDTO)
        {
            await _transactionCategory.UpdateTransactionCategoryAsync(id, transactionCategoryDTO);
        }
                
    }
}