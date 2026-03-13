using Domain.Entities;
using Application.DTO;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

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
            // 1. Validate Atomic Balance Rule
            decimal totalDebits = transactionDTO.Entries.Sum(e => e.Debit);
            decimal totalCredits = transactionDTO.Entries.Sum(e => e.Credit);
            if (totalDebits != totalCredits)
            {
                throw new InvalidOperationException("Transaction is unbalanced. Total debits must equal total credits.");
            }

            // 2. Update Account Balances
            foreach (var entry in transactionDTO.Entries.Where(e => e.AccountId.HasValue))
            {
                var account = await _accountRepository.GetAccountByIdAsync(entry.AccountId!.Value);
                if (account != null)
                {
                    // Assuming typical Asset account representation: Balance += (Debit - Credit)
                    account.Balance = (account.Balance ?? 0) + (entry.Debit - entry.Credit);
                    await _accountRepository.UpdateAccountAsync(account.Id, new AccountUpdateDTO 
                    { 
                        Name = account.Name,
                        Type = account.Type?.Name,
                        Balance = account.Balance.GetValueOrDefault(),
                        Status = account.Status
                    });
                }
            }

            // 3. Create Transaction
            transactionDTO.CreatedAt = DateTime.UtcNow;
            await _transactionRepository.CreateTransactionAsync(transactionDTO);
        }

        public async Task UpdateTransactionAsync(int id, UpdateTransactionDTO transactionDTO)
        {
            var oldTransaction = await _transactionRepository.GetTransactionByIdAsync(id);
            if (oldTransaction == null) return;

            // 1. Validate Atomic Balance Rule
            decimal totalDebits = transactionDTO.Entries.Sum(e => e.Debit);
            decimal totalCredits = transactionDTO.Entries.Sum(e => e.Credit);
            if (totalDebits != totalCredits)
            {
                throw new InvalidOperationException("Transaction is unbalanced. Total debits must equal total credits.");
            }

            // 2. Reverse old effect
            foreach (var oldEntry in oldTransaction.Entries.Where(e => e.AccountId.HasValue))
            {
                var account = await _accountRepository.GetAccountByIdAsync(oldEntry.AccountId!.Value);
                if (account != null)
                {
                    // Reverse Asset logic: Balance -= (Debit - Credit)
                    account.Balance = (account.Balance ?? 0) - (oldEntry.Debit - oldEntry.Credit);
                    await _accountRepository.UpdateAccountAsync(account.Id, new AccountUpdateDTO 
                    { 
                        Name = account.Name,
                        Type = account.Type?.Name,
                        Balance = account.Balance.GetValueOrDefault(),
                        Status = account.Status
                    });
                }
            }

            // 3. Apply new effect
            foreach (var entry in transactionDTO.Entries.Where(e => e.AccountId.HasValue))
            {
                var account = await _accountRepository.GetAccountByIdAsync(entry.AccountId!.Value);
                if (account != null)
                {
                    // Apply Asset logic: Balance += (Debit - Credit)
                    account.Balance = (account.Balance ?? 0) + (entry.Debit - entry.Credit);
                    await _accountRepository.UpdateAccountAsync(account.Id, new AccountUpdateDTO 
                    { 
                        Name = account.Name,
                        Type = account.Type?.Name,
                        Balance = account.Balance.GetValueOrDefault(),
                        Status = account.Status
                    });
                }
            }

            // 4. Update Transaction
            await _transactionRepository.UpdateTransactionAsync(id, transactionDTO);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
            if (transaction == null) return;

            // 1. Reverse effect
            foreach (var oldEntry in transaction.Entries.Where(e => e.AccountId.HasValue))
            {
                var account = await _accountRepository.GetAccountByIdAsync(oldEntry.AccountId!.Value);
                if (account != null)
                {
                    // Reverse Asset logic: Balance -= (Debit - Credit)
                    account.Balance = (account.Balance ?? 0) - (oldEntry.Debit - oldEntry.Credit);
                    await _accountRepository.UpdateAccountAsync(account.Id, new AccountUpdateDTO 
                    { 
                        Name = account.Name,
                        Type = account.Type?.Name,
                        Balance = account.Balance.GetValueOrDefault(),
                        Status = account.Status
                    });
                }
            }

            // 2. Delete Transaction
            await _transactionRepository.DeleteTransactionAsync(id);
        }
    }
}