using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.ValueObjects;

namespace Infrastructure.Repositories 
{
    public class TransactionCategoryRepository : ITransactionCategory
    {
      private readonly ApplicationDbContext _dbContext;

      public TransactionCategoryRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

      //Retrieving TransactionCategories

      public async Task<List<TransactionCategory>> GetAllTransactionCategoriesAsync()
        {
            return await _dbContext.TransactionCategories.ToListAsync();
        }

        public async Task<TransactionCategory?> GetTransactionCategoryByIdAsync(int id)
        {
            return await _dbContext.TransactionCategories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateTransactionCategoryAsync(TransactionCategoryCreateDTO TransactionCategoryDTO)
        {
            TransactionCategory TransactionCategory = new()
            {
                Name = TransactionCategoryDTO.Name,
                Type = TransactionCategoryDTO.Type != null ? TransactionType.FromString(TransactionCategoryDTO.Type) : null,
            };
            _dbContext.TransactionCategories.Add(TransactionCategory);
            await _dbContext.SaveChangesAsync();
        }
    

    public async Task UpdateTransactionCategoryAsync(int id, TransactionCategoryUpdateDTO TransactionCategoryDTO)
        {
            var TransactionCategory = await _dbContext.TransactionCategories.FindAsync(id);
            if (TransactionCategory == null) return;

            TransactionCategory.Name = TransactionCategoryDTO.Name;
            TransactionCategory.Type = TransactionCategoryDTO.Type != null ? TransactionType.FromString(TransactionCategoryDTO.Type) : null;
            
            await _dbContext.SaveChangesAsync();
        }
    }
}