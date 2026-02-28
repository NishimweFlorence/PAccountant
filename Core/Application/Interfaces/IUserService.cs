using Domain.Entities;
using Application.DTO;

namespace Application.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();
        User? GetUserById(int id);
        void CreateUser(UserCreateDTO userDto);
        User? ValidateUser(UserLoginDTO loginDto);
        void UpdateUser(int id, UserUpdateDTO userDto);
        void DeleteUser(UserDeleteDTO dto);
    }
}
