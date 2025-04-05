using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Entity;
using TgMiniAppAuth;
using TgMiniAppAuth.AuthContext;

namespace MniApps_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = TgMiniAppAuthConstants.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly ITelegramUserAccessor _telegramUserAccessor;
        private readonly IUserService _userService;

        public UserController(ITelegramUserAccessor telegramUserAccessor, IUserService userService)
        {
            _telegramUserAccessor = telegramUserAccessor;
            _userService = userService;
        }

        [HttpGet("users/auth")]
        public IActionResult GetUser()
        {
            return Ok(_telegramUserAccessor.User);
        }

        [HttpPost("users/auth")]
        public IActionResult AuthUser([FromBody] User user)
        {
            var telegramId = _telegramUserAccessor.User.Id;
            
            if (telegramId != null)
            {
                _userService.AddUser(user, telegramId);
                
                return Ok();
            }

            return BadRequest();
        }
    }
}
