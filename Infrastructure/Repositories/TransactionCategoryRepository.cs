using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

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

      public List<TransactionCategory> GetAllTransactionCategories()
        {
           List<TransactionCategory> transactionCategories = _dbContext.TransactionCategories.ToList();
              return transactionCategories;
             
        }

        public TransactionCategory GetTransactionCategoryById(int id)
        {
            return _dbContext.TransactionCategories.FirstOrDefault(c => c.Id == id);
        }

        public void CreateTransactionCategory(TransactionCategoryCreateDTO TransactionCategoryDTO)
        {
            TransactionCategory TransactionCategory = new()
            {
                Name = TransactionCategoryDTO.Name,
                Type = TransactionCategoryDTO.Type,
                
                
            
            };
            _dbContext.TransactionCategories.Add(TransactionCategory);
            _dbContext.SaveChanges();
        }
    

    public void UpdateTransactionCategory(int id, TransactionCategoryUpdateDTO TransactionCategoryDTO)
        {
            var TransactionCategory = _dbContext.TransactionCategories.Find(id);
            if (TransactionCategory == null) return;
            {
                TransactionCategory.Name = TransactionCategoryDTO.Name;
                TransactionCategory.Type = TransactionCategoryDTO.Type;
                
                _dbContext.SaveChanges();
            }
        }
    }
}