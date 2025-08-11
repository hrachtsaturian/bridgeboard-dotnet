using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            return await _context.Users
            .Select(u => new UserReadDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            }).ToListAsync();
        }

        public async Task<UserReadDto?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return new UserReadDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public async Task<UserReadDto> CreateUserAsync(UserCreateDto userDto)
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = userDto.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserReadDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public async Task<UserReadDto?> UpdateUserAsync(int id, UserUpdateDto userDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user.FirstName = userDto.FirstName ?? user.FirstName;
            user.LastName = userDto.LastName ?? user.LastName;
            user.Email = userDto.Email ?? user.Email;
            user.Password = userDto.Password ?? user.Password;

            await _context.SaveChangesAsync();

            return new UserReadDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}