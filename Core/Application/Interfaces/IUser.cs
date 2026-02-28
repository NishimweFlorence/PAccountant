using Domain.Entities;
using Application.DTO;

namespace Application.Interfaces
{
    public interface IUser
    {
        List<User> GetUsers();
        User? GetUserById(int id);
        User? GetUserByEmail(string email);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(UserDeleteDTO dto);
    }
}
