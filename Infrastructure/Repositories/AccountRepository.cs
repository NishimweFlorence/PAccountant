using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories 
{
    public class AccountRepository : IAccount
    {
      private readonly ApplicationDbContext _dbContext;

      public AccountRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

      //Retrieving Accounts

      public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _dbContext.Accounts.ToListAsync();
        }

        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            return await _dbContext.Accounts.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateAccountAsync(AccountCreateDTO AccountDTO)
        {
            var currency = AccountDTO.Currency != null ? Currency.FromCode(AccountDTO.Currency) : null;
            Account Account = new()
            {
                Name = AccountDTO.Name,
                AccountNumber = AccountDTO.AccountNumber,
                Type = AccountDTO.Type != null ? AccountType.FromString(AccountDTO.Type) : null,
                Balance = AccountDTO.Balance,
                Currency = currency,
                Status = AccountDTO.Status,
                CreatedAt = DateTime.Now
            };
            _dbContext.Accounts.Add(Account);
            await _dbContext.SaveChangesAsync();

            if (Account.Balance > 0)
            {
                var equityCat = await _dbContext.TransactionCategories.FirstOrDefaultAsync(c => c.Name == "Starting Balance");
                if (equityCat == null)
                {
                    equityCat = new TransactionCategory { Name = "Starting Balance", Type = TransactionType.FromString("Equity") };
                    _dbContext.TransactionCategories.Add(equityCat);
                    await _dbContext.SaveChangesAsync();
                }

                var transaction = new Transaction
                {
                    TransactionDate = DateTime.Now,
                    Description = $"Initial Balance for Account: {Account.Name}",
                    Currency = Currency.FromCode(Account.Currency!.Code),
                    CreatedAt = DateTime.Now,
                    Entries = new List<TransactionEntry>
                    {
                        new TransactionEntry { AccountId = Account.Id, Debit = Account.Balance.GetValueOrDefault(), Credit = 0 },
                        new TransactionEntry { CategoryId = equityCat.Id, Debit = 0, Credit = Account.Balance.GetValueOrDefault() }
                    }
                };
                _dbContext.Transactions.Add(transaction);
                await _dbContext.SaveChangesAsync();
            }
        }
    

    public async Task UpdateAccountAsync(int id, AccountUpdateDTO AccountDTO)
        {
            var Account = await _dbContext.Accounts.FindAsync(id);
            if (Account == null) return;
            
            Account.Name = AccountDTO.Name;
            Account.AccountNumber = AccountDTO.AccountNumber;
            Account.Type = AccountDTO.Type != null ? AccountType.FromString(AccountDTO.Type) : null;
            Account.Balance = AccountDTO.Balance;
            Account.Status = AccountDTO.Status;
            await _dbContext.SaveChangesAsync();
        }
    }
    
}