using HealthWellness.Data;
using HealthWellness.DTOs;
using HealthWellness.Interfaces;
using HealthWellness.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthWellness.Services
{
    public class AuthService : IAuthService
    {
        private readonly WellnessDbContext _db;
        private readonly IConfiguration _config;

        public AuthService(WellnessDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public User Register(RegisterDto dto)
        {
            if (_db.Users.Any(u => u.Username == dto.Username))
                throw new Exception("Username already exists");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User"
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            return user;
        }

        public AuthResponseDto? Login(LoginDto dto)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == dto.Username);

            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = user.Username,
                Role = user.Role
            };
        }

        public User? GetProfile(string username)
            => _db.Users.FirstOrDefault(u => u.Username == username);
    }
}
