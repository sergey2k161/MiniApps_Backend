using Microsoft.AspNetCore.Mvc;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;

namespace MiniApps_Backend.API.Controllers
{
    /// <summary>
    /// Контроллер пользователей
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Конструтор контроллера пользователей
        /// </summary>
        /// <param name="userService"></param>
        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        /// <summary>
        /// Получение полльзователя или создание его
        /// </summary>
        /// <param name="request">Модель запроса пользователя</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetOrCreateUser([FromBody] UserRequest request)
        {
            await _userService.AddUser(request);
            return Ok();
        }

        /// <summary>
        /// Получение информации о пользователе по его Telegram ID
        /// </summary>
        /// <param name="telegramId">Идентификатор пользователя</param>
        /// <returns>Возвращает информацию о пользователе</returns>
        [HttpGet("{telegramId}")]
        public async Task<IActionResult> GetUserByTelegramId(long telegramId)
        {
            var user = await _userService.GetUserByTelegramId(telegramId);

            if (user == null) 
            {
                return BadRequest("Пользователь не зарегестрирован");
            }

            return Ok(user);
        }

        /// <summary>
        /// Получение списка курсов, на которые пользователь подписан
        /// </summary>
        /// <param name="telegramId">Идентификатор пользователя</param>
        /// <returns>Возвращает список курсов, на которые пользователь подписан</returns>
        [HttpGet("list-subscription-courses")]
        public async Task<IActionResult> ListSubscriptionCourses([FromQuery] long telegramId)
        {
            var subs = await _userService.GetSubscribesList(telegramId);

            return Ok(subs);
        }

        /// <summary>
        /// Получение ролей пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <returns>Список ролей</returns>
        //[Authorize]
        [HttpGet("roles/{userId}")]
        public async Task<IActionResult> GetUserRoles(Guid userId)
        {
            try
            {
                var roles = await _userService.GetUserRoles(userId);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Изменение роли пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="roleName">Название новой роли</param>
        /// <returns>Результат операции</returns>
        [HttpPost("changeRole")]
        public async Task<IActionResult> ChangeUserRole([FromQuery] Guid userId, [FromQuery] string roleName)
        {
            try
            {
                var result = await _userService.ChangeUserRole(userId, roleName);
                if (!result.IsSuccess)
                {
                    return BadRequest(result.Errors);
                }

                return Ok(new { message = "Роль успешно изменена" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Login([FromQuery] long telegramId)
        {
            var user = await _userService.GetUserByTelegramId(telegramId);

            if (user == null)
            {
                return Unauthorized("Пользователь не найден");
            }

            var token = TokenManager.GenerateJwtToken(user, _configuration);

            if (token == null)
            {
                return BadRequest($"Ошибка генерации токена.");
            }

            return Ok(token);
        }

        [HttpPatch("notification-switch")]
        public async Task<IActionResult> NotificationSwitch([FromQuery] long telegramId)
        {
            var result = await _userService.NotificationSwitch(telegramId);

            return Ok(result);
        }

        [HttpPatch("notification-frequency")]
        public async Task<IActionResult> ChangeNotificationFrequency([FromQuery] long telegramId, [FromQuery] int frequency)
        {
            var result = await _userService.ChangeNotificationFrequency(telegramId, frequency);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto model)
        {
            var result = await _userService.UpdateUser(model);

            return Ok(result);
        }

        [HttpPatch("active-course")]
        public async Task<IActionResult> SwitchActiveCourse(long telegramId)
        {
            await _userService.SwitchActiveCourse(telegramId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();

            if (users == null)
            {
                return BadRequest("Пользователи не найдены");
            }

            return Ok(users);
        }

        [HttpGet("is-active-block-course")]
        public async Task<IActionResult> GetActiveBlockForCourse([FromQuery] long telegramId, Guid blockId)
        {
            var result = await _userService.GetActiveBlockForCourse(telegramId, blockId);

            return Ok(result);
        }

    }

}


