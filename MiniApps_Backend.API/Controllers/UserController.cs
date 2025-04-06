using Microsoft.AspNetCore.Mvc;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;

namespace MniApps_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = TgMiniAppAuthConstants.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> GetOrCreateUser([FromBody] UserRequest request)
        {
            await _userService.AddUser(request);
            return Ok();
        }

    }

}


