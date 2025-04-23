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
        private readonly IUserService _userService;

        public TokenManager(IUserService userService)
        {
            _userService = userService;
        }

        public string GenerateJwtToken(User user, IConfiguration configuration)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roleTask = _userService.GetUserRoles(user.CommonUserId.Value);
            var role = roleTask.Result.FirstOrDefault() ?? string.Empty; 

            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim("telegramId", user.TelegramId.ToString()),
                    new Claim(ClaimTypes.Role, role),
                };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
