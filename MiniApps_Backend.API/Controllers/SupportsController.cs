﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.Business.Services.Logic;
using MiniApps_Backend.DataBase.Models.Dto;

namespace MiniApps_Backend.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с обращениями
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SupportsController : ControllerBase
    {
        private readonly ISupportService _supportService;
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="supportService"></param>
        public SupportsController(ISupportService supportService, INotificationService notificationService)
        {
            _supportService = supportService;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Создание обращения
        /// </summary>
        /// <param name="telegramId">Телеграмм Id пользователя</param>
        /// <param name="message">Сообщение обращения</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateSupport(long telegramId, string message)
        {
            var result = await _supportService.CreateSupport(telegramId, message);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Получение всех обращений
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Analyst")]
        [HttpGet]
        public async Task<IActionResult> GetSupports()
        {
            var result = await _supportService.GetSupports();

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        /// <summary>
        /// Взять обращение в работу
        /// </summary>
        /// <param name="id">Ид обращения</param>
        /// <param name="helper">Ид работника</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Analyst")]
        [HttpPatch("take-appeal")]
        public async Task<IActionResult> TakeAppeal(Guid id, long helper)
        {
            var result = await _supportService.TakeAppeal(id, helper);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Изменить помощника
        /// </summary>
        /// <param name="id">Ид обращения</param>
        /// <param name="helper">Ид нового помошника</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Analyst")]
        [HttpPatch("change-helper")]
        public async Task<IActionResult> ChangeHelper(Guid id, long helper)
        {
            var result = await _supportService.ChangeHelper(id, helper);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Изменить процесс обращения
        /// </summary>
        /// <param name="id">Ид обращения</param>
        /// <param name="process">Название процесса</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Analyst")]
        [HttpPatch("change-process")]
        public async Task<IActionResult> ChangeProcess(Guid id, string process)
        {
            var result = await _supportService.ChangeProcess(id, process);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Изменить статус обращения
        /// </summary>
        /// <param name="id">Ид обращения</param>
        /// <param name="newStatus">Новый статус/param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Analyst")]
        [HttpPatch("change-status")]
        public async Task<IActionResult> ChangeStatus(Guid id, string newStatus)
        {
            var result = await _supportService.ChangeStatus(id, newStatus);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Получить обращение по Ид
        /// </summary>
        /// <param name="id">Ид обращения</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Analyst")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupportById(Guid id)
        {
            var result = await _supportService.GetSupportById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessageToUser([FromBody] SendMessage model)
        {
            await _notificationService.SendNotificationAsync(model.TelegramId, model.Message);

            return Ok();
        }
    }
}
