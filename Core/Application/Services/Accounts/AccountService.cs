using System.Security.Cryptography.X509Certificates;
using Application.Interfaces;
using Domain.Entities;
using Application.DTO;


namespace Application.Services.Accounts
{
    
    public class AccountService : IAccountService
    {
        private readonly IAccount _account;

        //Constructor
        public AccountService(IAccount account)
        {
            _account = account;
        }
        
            public List<Account> GetAllAccounts()
            {
                List<Account> accounts = _account.GetAllAccounts();
               return accounts;
            }

            public Account GetAccountById(int id)
            {
               return _account.GetAccountById(id);
            }   

            public void CreateAccount(AccountCreateDTO accountDTO)
            {  
              _account.CreateAccount(accountDTO);
            }

            public void UpdateAccount(int id, AccountUpdateDTO accountDTO)
            {
               _account.UpdateAccount(id, accountDTO);
            }
    }
}