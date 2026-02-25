using Application.DTO;
using Application.Interfaces;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface ITransactionCategory
    {
        public List<TransactionCategory> GetAllTransactionCategories();

        public TransactionCategory GetTransactionCategoryById(int id);

        void CreateTransactionCategory(TransactionCategoryCreateDTO transactionCategoryCreateDTO);
        void UpdateTransactionCategory(int id,TransactionCategoryUpdateDTO transactionCategoryUpdateDTO);
    }
}