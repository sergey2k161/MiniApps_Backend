
using Microsoft.AspNetCore.Mvc;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto.CourseConstructor;

namespace MiniApps_Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly INotificationService _notificationService;

        public NotificationsController(ICourseService courseService, INotificationService notificationService)
        {
            _courseService = courseService;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Отправка материалов по ключу триггера и чату
        /// </summary>
        /// <param name="request">Данные запроса для отправки материалов</param>
        /// <returns>Возвращает статус выполнения операции</returns>
        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] MaterialRequestDto request)
        {
            await _courseService.SendMaterialsByTriggerAsync(request.TriggerKey, request.ChatId);
            return Ok();
        }

        /// <summary>
        /// Добавление нового материала в курс
        /// </summary>
        /// <param name="material">Данные материала, который нужно добавить</param>
        /// <returns>Возвращает статус выполнения операции</returns>
        [HttpPost("material")]
        public async Task<IActionResult> AddMaterial([FromBody] CourseMaterial material)
        {
            await _courseService.AddMeterial(material);

            return Ok();
        }

        /// <summary>
        /// Отправка уведомлений всем пользователям
        /// </summary>
        /// <param name="message">Сообщение для отправки</param>
        /// <returns>Возвращает статус выполнения операции</returns>
        [HttpPost("send-notifications")]
        public async Task<IActionResult> SendNotificationsToAll([FromQuery] string message)
        {
            await _notificationService.SendNotificationsToAllAsync(message);

            return Ok();
        }
    }
}
