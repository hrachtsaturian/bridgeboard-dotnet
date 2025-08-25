using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Api.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> SignupAsync(SignupRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }

    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AuthResponse> SignupAsync(SignupRequest request)
        {

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists");
            }
            var hasher = new PasswordHasher<object>();
            var hashedPassword = hasher.HashPassword(new object(), request.Password);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = hashedPassword
            };

            _context.Users.Add(user);
            var res = await _context.SaveChangesAsync();

            return new AuthResponse
            {
                UserId = user.Id,

            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid email or password");
            }

            var hasher = new PasswordHasher<object>();
            var result = hasher.VerifyHashedPassword(new object(), user.Password, request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new InvalidOperationException("Invalid email or password");
            }

            return new AuthResponse
            {
                UserId = user.Id,
            };
        }
    }
}