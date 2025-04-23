using Microsoft.IdentityModel.Tokens;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniApps_Backend.API.Extensions
{
    public class TokenManager
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public TokenManager(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        public async Task<string> GenerateJwtToken(Guid userId, IConfiguration configuration)
        {
            // Получаем роли пользователя
            var roles = await _userService.GetUserRoles(userId);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim("UserId", userId.ToString())
            };

            // Добавляем каждую роль как отдельный Claim
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); 
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), 
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
