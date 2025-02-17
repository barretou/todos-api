using Microsoft.EntityFrameworkCore;
using TodoApi.Context;
using TodoApi.DTO.Users;
using TodoApi.Models.Entities;

namespace TodoApi.Services
{
    public class UserService
    {
        private readonly ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");

            return user;
        }

        public async Task<UserModel> CreateUserAsync(CreateUserDTO DTO)
        {
            if (DTO == null)
                throw new ArgumentNullException(nameof(DTO));

            var user = new UserModel
            {
                Name = DTO.Name,
                Email = DTO.Email
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> UpdateUserAsync(int id, UpdateUserDTO DTO)
        {
            if (DTO == null)
                throw new ArgumentNullException(nameof(DTO));

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");

            user.Name = DTO.Name;
            user.Email = DTO.Email;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
