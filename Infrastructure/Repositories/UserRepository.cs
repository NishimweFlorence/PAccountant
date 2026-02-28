using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Data;
using Application.DTO;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUser
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.IsActive = user.IsActive;
                existingUser.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void DeleteUser(UserDeleteDTO dto)
        {
            var user = _context.Users.Find(dto.Id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
