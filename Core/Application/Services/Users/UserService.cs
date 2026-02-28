using Domain.Entities;
using Application.Interfaces;
using Application.DTO;

namespace Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUser _userRepository;

        public UserService(IUser userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public User? GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public void CreateUser(UserCreateDTO userDto)
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                PasswordHash = HashPassword(userDto.Password),
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _userRepository.CreateUser(user);
        }

        public User? ValidateUser(UserLoginDTO loginDto)
        {
            var user = _userRepository.GetUserByEmail(loginDto.Email);
            if (user == null) return null;

             if (user.PasswordHash == HashPassword(loginDto.Password))
            {
                return user;
            }
            return null;
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public void UpdateUser(int id, UserUpdateDTO userDto)
        {
            var user = new User
            {
                Id = id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                PasswordHash = string.Empty,
                IsActive = userDto.IsActive
            };
            _userRepository.UpdateUser(user);
        }

        public void DeleteUser(UserDeleteDTO dto)
        {
            _userRepository.DeleteUser(dto);
        }
    }
}
