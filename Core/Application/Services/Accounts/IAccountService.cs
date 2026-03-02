using Application.DTO;
using Application.Interfaces;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface IAccountService
    {
         List<Account> GetAllAccounts();

        Account GetAccountById(int id);

        void CreateAccount(AccountCreateDTO accountCreateDTO);
        void UpdateAccount(int id,AccountUpdateDTO accountUpdateDTO);
    }
}