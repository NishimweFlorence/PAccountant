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
        
            public List<TransactionCategory> GetAllTransactionCategories()
            
            {
                List<TransactionCategory> transactionCategories = _transactionCategory.GetAllTransactionCategories();
               return transactionCategories;
            }

            public TransactionCategory GetTransactionCategoryById(int id)
            {
               return _transactionCategory.GetTransactionCategoryById(id);
            }   

            public void CreateTransactionCategory(TransactionCategoryCreateDTO transactionCategoryDTO)
            {  
              _transactionCategory.CreateTransactionCategory(transactionCategoryDTO);
            }

            public void UpdateTransactionCategory(int id, TransactionCategoryUpdateDTO transactionCategoryDTO)
            {
               _transactionCategory.UpdateTransactionCategory(id, transactionCategoryDTO);
            }
                
    }
}