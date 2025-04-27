using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniApps_Backend.API.Extensions;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;
using StackExchange.Redis;

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
        private readonly IDatabase _redisDatabase;
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// Конструтор контроллера пользователей
        /// </summary>
        /// <param name="userService"></param>
        public UsersController(IUserService userService, IConfiguration configuration, IDatabase redisDatabase, TokenManager tokenManager)
        {
            _userService = userService;
            _configuration = configuration;
            _redisDatabase = redisDatabase;
            _tokenManager = tokenManager;
        }

        /// <summary>
        /// Получение полльзователя или создание его
        /// </summary>
        /// <param name="request">Модель запроса пользователя</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Analyst")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// Включение/выключение уведомлений
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [HttpPatch("notification-switch")]
        public async Task<IActionResult> NotificationSwitch([FromQuery] long telegramId)
        {
            var result = await _userService.NotificationSwitch(telegramId);

            return Ok(result);
        }

        /// <summary>
        /// Изменение частоты уведомлений
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="frequency"></param>
        /// <returns></returns>
        [HttpPatch("notification-frequency")]
        public async Task<IActionResult> ChangeNotificationFrequency([FromQuery] long telegramId, [FromQuery] int frequency)
        {
            var result = await _userService.ChangeNotificationFrequency(telegramId, frequency);

            return Ok(result);
        }

        /// <summary>
        /// Изменение информации о пользователе
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto model)
        {
            var result = await _userService.UpdateUser(model);

            return Ok(result);
        }

        /// <summary>
        /// Переключение активного курса
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [HttpPatch("active-course")]
        public async Task<IActionResult> SwitchActiveCourse(long telegramId)
        {
            await _userService.SwitchActiveCourse(telegramId);

            return Ok();
        }

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Получение активного блока курса
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="blockId"></param>
        /// <returns></returns>
        [HttpGet("is-active-block-course")]
        public async Task<IActionResult> GetActiveBlockForCourse([FromQuery] long telegramId, Guid blockId)
        {
            var result = await _userService.GetActiveBlockForCourse(telegramId, blockId);

            return Ok(result);
        }

        /// <summary>
        /// Проверка ключа администратора
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("api/auth/validate-key")]
        public async Task<IActionResult> ValidateAdminKey([FromBody] LoginDto login)
        {
            var redisValue = await _redisDatabase.StringGetAsync($"admin_key:{login.Key}");
            if (redisValue.IsNullOrEmpty)
            {
                return Unauthorized("Ключ недействителен или истек.");
            }

            var user = await _userService.GetUserByTelegramId(login.TelegramId);
            if (user == null)
            {
                return Unauthorized("Пользователь не найден.");
            }
            // Генерация JWT токена
            var token = await _tokenManager.GenerateJwtToken((Guid)user.CommonUserId, _configuration);

            return Ok(token);
        }
    }

}


