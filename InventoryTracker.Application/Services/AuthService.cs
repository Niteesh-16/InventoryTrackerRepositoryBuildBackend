using InventoryTracker.Application.DTOs;
using InventoryTracker.Application.Interfaces;
using InventoryTracker.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;


namespace InventoryTracker.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userRepo.GetByUsernameAsync(dto.Username);

            if (existingUser != null)
            {
                throw new Exception("Username already exists");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var adminSecret = _config["AdminSettings:SecretCode"];

            string role = dto.AdminCode == adminSecret ? "Admin" : "Staff";

            var user = new User
            {
                Username = dto.Username,
                Password = hashedPassword,
                Role = role
            };

            await _userRepo.AddAsync(user);
            await _userRepo.SaveAsync();
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userRepo.GetByUsernameAsync(dto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                throw new Exception("Invalid credentials");

            return GenerateToken(user);
        }

        private string GenerateToken(User user) 
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
