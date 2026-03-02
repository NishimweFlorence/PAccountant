using System.Collections.Generic;
using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITransaction
    {
        Transaction GetTransactionById(int Id);
        List<Transaction> GetAllTransactions();
        void CreateTransaction(CreateTransactionDTO TransactionDTO);
        void UpdateTransaction(int Id, UpdateTransactionDTO TransactionDTO);
    }
}
