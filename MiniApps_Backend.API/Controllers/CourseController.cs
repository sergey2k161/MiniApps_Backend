using Microsoft.AspNetCore.Mvc;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase;
using MiniApps_Backend.DataBase.Models.Dto.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity.ManyToMany;

namespace MiniApps_Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        /// <summary>
        /// Получение всех курсов
        /// </summary>
        /// <returns>Возвращает список всех курсов</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _courseService.GetCourses();

            return Ok(courses);
        }

        /// <summary>
        /// Получение курса по его идентификатору
        /// </summary>
        /// <param name="courseid">Идентификатор курса</param>
        /// <returns>Возвращает курс с указанным идентификатором</returns>
        [HttpGet]
        public async Task<IActionResult> GetCourseById(Guid courseid)
        {
            var course = await _courseService.GetCourseById(courseid);

            return Ok(course);
        }

        /// <summary>
        /// Создание нового курса
        /// </summary>
        /// <param name="model">Данные для создания курса</param>
        /// <returns>Возвращает статус выполнения операции</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CourseDto model)
        {
            await _courseService.CreateCourse(model);

            return Ok();
        }

        /// <summary>
        /// Подписка пользователя на курс
        /// </summary>
        /// <param name="subscription">Информация о подписке пользователя на курс</param>
        /// <returns>Возвращает статус выполнения операции</returns>
        [HttpPost("subscribe")]
        public async Task<IActionResult> SubscribeToCourse([FromBody] CourseSubscriptionDto subscription)
        {
            try
            {
                await _courseService.SubscribeToCourse(subscription.CourseId, subscription.TelegramId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Проверка, подписан ли пользователь на курс
        /// </summary>
        /// <param name="telegramId">Идентификатор пользователя в Telegram</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Возвращает информацию о том, подписан ли пользователь на курс</returns>
        [HttpGet("isSubscribe")]
        public async Task<IActionResult> IsSubscribe([FromQuery] long telegramId, [FromQuery] Guid courseId)
        {
            var sub = await _courseService.UserIsSubscribe(telegramId, courseId);

            return Ok(sub);
        }

        /// <summary>
        /// Получение уроков по идентификатору курса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Возвращает список уроков для указанного курса</returns>
        [HttpGet("lessonByCourse")]
        public async Task<IActionResult> GetLessonsByCourseId([FromQuery] Guid courseId)
        {
            return Ok(await _courseService.GetLessonsByCourseId(courseId));
        }

        /// <summary>
        /// Получение вопросов по идентификатору теста
        /// </summary>
        /// <param name="testId">Идентификатор теста</param>
        /// <returns>Возвращает список вопросов для указанного теста</returns>
        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestionsByTestId([FromQuery] Guid testId)
        {
            var q = await _courseService.GetQuestionsByTestId(testId);

            return Ok(q);
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
    }

}
