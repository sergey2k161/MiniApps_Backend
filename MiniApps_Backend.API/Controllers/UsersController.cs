﻿using Microsoft.AspNetCore.Mvc;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;

namespace MiniApps_Backend.API.Controllers
{
    /// <summary>
    /// Контроллер пользователей
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Конструтор контроллера пользователей
        /// </summary>
        /// <param name="userService"></param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/users
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

        [HttpGet]
        public async Task<IActionResult> GetUserByTelegramId([FromQuery] long telegramId)
        {
            var user = await _userService.GetUserByTelegramId(telegramId);

            return Ok(user);
        }

    }

}


