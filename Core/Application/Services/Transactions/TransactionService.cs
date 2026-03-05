using Domain.Entities;
using Application.DTO;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Application.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransaction _transactionRepository;
        private readonly IAccount _accountRepository;
        private readonly ITransactionCategory _categoryRepository;

        public TransactionService(
            ITransaction transactionRepository,
            IAccount accountRepository,
            ITransactionCategory categoryRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllTransactionsAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _transactionRepository.GetTransactionByIdAsync(id);
        }

        public async Task CreateTransactionAsync(CreateTransactionDTO transactionDTO)
        {
            // 1. Get Category Effect
            var category = await _categoryRepository.GetTransactionCategoryByIdAsync(transactionDTO.IdTransactionCategory);
            int effect = category?.Type?.Effect ?? 0;

            // 2. Update Account Balance
            var account = await _accountRepository.GetAccountByIdAsync(transactionDTO.AccountId);
            if (account != null && effect != 0)
            {
                account.Balance = (account.Balance ?? 0) + (transactionDTO.Amount * effect);
                await _accountRepository.UpdateAccountAsync(account.Id, new AccountUpdateDTO 
                { 
                    Name = account.Name,
                    Type = account.Type,
                    Balance = account.Balance.GetValueOrDefault(),
                    Status = account.Status
                });
            }

            // 3. Create Transaction
            await _transactionRepository.CreateTransactionAsync(transactionDTO);
        }

        public async Task UpdateTransactionAsync(int id, UpdateTransactionDTO transactionDTO)
        {
            var oldTransaction = await _transactionRepository.GetTransactionByIdAsync(id);
            if (oldTransaction == null) return;

            // 1. Reverse old effect
            var oldCategory = await _categoryRepository.GetTransactionCategoryByIdAsync(oldTransaction.IdTransactionCategory);
            int oldEffect = oldCategory?.Type?.Effect ?? 0;
            var oldAccount = await _accountRepository.GetAccountByIdAsync(oldTransaction.AccountId);
            if (oldAccount != null && oldEffect != 0)
            {
                oldAccount.Balance = (oldAccount.Balance ?? 0) - (oldTransaction.Amount * oldEffect);
                await _accountRepository.UpdateAccountAsync(oldAccount.Id, new AccountUpdateDTO 
                { 
                    Name = oldAccount.Name,
                    Type = oldAccount.Type,
                    Balance = oldAccount.Balance.GetValueOrDefault(),
                    Status = oldAccount.Status
                });
            }

            // 2. Apply new effect
            var newCategory = await _categoryRepository.GetTransactionCategoryByIdAsync(transactionDTO.IdTransactionCategory);
            int newEffect = newCategory?.Type?.Effect ?? 0;
            var newAccount = await _accountRepository.GetAccountByIdAsync(transactionDTO.AccountId);
            if (newAccount != null && newEffect != 0)
            {
                newAccount.Balance = (newAccount.Balance ?? 0) + (transactionDTO.Amount * newEffect);
                await _accountRepository.UpdateAccountAsync(newAccount.Id, new AccountUpdateDTO 
                { 
                    Name = newAccount.Name,
                    Type = newAccount.Type,
                    Balance = newAccount.Balance.GetValueOrDefault(),
                    Status = newAccount.Status
                });
            }

            // 3. Update Transaction
            await _transactionRepository.UpdateTransactionAsync(id, transactionDTO);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
            if (transaction == null) return;

            // 1. Reverse effect
            var category = await _categoryRepository.GetTransactionCategoryByIdAsync(transaction.IdTransactionCategory);
            int effect = category?.Type?.Effect ?? 0;
            var account = await _accountRepository.GetAccountByIdAsync(transaction.AccountId);
            if (account != null && effect != 0)
            {
                account.Balance = (account.Balance ?? 0) - (transaction.Amount * effect);
                await _accountRepository.UpdateAccountAsync(account.Id, new AccountUpdateDTO 
                { 
                    Name = account.Name,
                    Type = account.Type,
                    Balance = account.Balance.GetValueOrDefault(),
                    Status = account.Status
                });
            }

            // 2. Delete Transaction
            await _transactionRepository.DeleteTransactionAsync(id);
        }
    }
}