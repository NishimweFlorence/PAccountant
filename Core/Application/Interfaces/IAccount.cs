using Application.DTO;
using Application.Interfaces;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface IAccount
    {
        public List<Account> GetAllAccounts();

        public Account GetAccountById(int id);

        void CreateAccount(AccountCreateDTO accountCreateDTO);
        void UpdateAccount(int id,AccountUpdateDTO accountUpdateDTO);
    }
}