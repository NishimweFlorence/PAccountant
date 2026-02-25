using Application.DTO;
using Application.Interfaces;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface ITransactionCategoryService
    {
         List<TransactionCategory> GetAllTransactionCategories();

        TransactionCategory GetTransactionCategoryById(int id);

        void CreateTransactionCategory(TransactionCategoryCreateDTO transactionCategoryCreateDTO);
        void UpdateTransactionCategory(int id,TransactionCategoryUpdateDTO transactionCategoryUpdateDTO);
    }
}