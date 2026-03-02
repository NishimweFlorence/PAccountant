using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

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

      public List<Account> GetAllAccounts()
        {
           List<Account> Accounts = _dbContext.Accounts.ToList();
              return Accounts;
             
        }

        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts.FirstOrDefault(c => c.Id == id);
        }

        public void CreateAccount(AccountCreateDTO AccountDTO)
        {
            Account Account = new()
            {
                Name = AccountDTO.Name,
                Type = AccountDTO.Type,
                balance = AccountDTO.balance,
                Currency = AccountDTO.Currency,
                Status = AccountDTO.Status,
                
            
            };
            _dbContext.Accounts.Add(Account);
            _dbContext.SaveChanges();
        }
    

    public void UpdateAccount(int id, AccountUpdateDTO AccountDTO)
        {
            var Account = _dbContext.Accounts.Find(id);
            if (Account == null) return;
            {
                Account.Name = AccountDTO.Name;
                Account.Type = AccountDTO.Type;
                Account.balance = AccountDTO.balance;
                Account.Status = AccountDTO.Status;
                _dbContext.SaveChanges();
            }
        }
    }
    
}