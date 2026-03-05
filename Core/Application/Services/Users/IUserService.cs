using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();
        User? GetUserById(int id);
        User? ValidateUser(UserLoginDTO loginDto);
        void CreateUser(UserCreateDTO userDto);
        void UpdateUser(int id, UserUpdateDTO userDto);
        void DeleteUser(UserDeleteDTO dto);
    }
}
