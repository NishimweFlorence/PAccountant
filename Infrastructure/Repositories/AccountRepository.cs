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
            Account Account = new()
            {
                Name = AccountDTO.Name,
                Type = AccountDTO.Type,
                Balance = AccountDTO.Balance,
                Currency = AccountDTO.Currency != null ? Currency.FromCode(AccountDTO.Currency) : null,
                Status = AccountDTO.Status,
                CreatedAt = DateTime.Now
            };
            _dbContext.Accounts.Add(Account);
            await _dbContext.SaveChangesAsync();
        }
    

    public async Task UpdateAccountAsync(int id, AccountUpdateDTO AccountDTO)
        {
            var Account = await _dbContext.Accounts.FindAsync(id);
            if (Account == null) return;
            
            Account.Name = AccountDTO.Name;
            Account.Type = AccountDTO.Type;
            Account.Balance = AccountDTO.Balance;
            Account.Status = AccountDTO.Status;
            await _dbContext.SaveChangesAsync();
        }
    }
    
}