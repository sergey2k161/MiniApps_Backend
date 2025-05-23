﻿using Microsoft.AspNetCore.Mvc;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase;
using MiniApps_Backend.DataBase.Models.Dto.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.Analysis;
using MiniApps_Backend.DataBase.Models.Entity.ManyToMany;

namespace MiniApps_Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
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
            var result = await _courseService.SubscribeToCourse(subscription.CourseId, subscription.TelegramId);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        /// <summary>
        /// Проверка, подписан ли пользователь на курс
        /// </summary>
        /// <param name="telegramId">Идентификатор пользователя в Telegram</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Возвращает информацию о том, подписан ли пользователь на курс</returns>
        [HttpGet("is-subscribe")]
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
        [HttpGet("blocks-by-course")]
        public async Task<IActionResult> GetBlocksByCourseId([FromQuery] Guid courseId)
        {
            return Ok(await _courseService.GetBlocksByCourseId(courseId));
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
        /// Отправка результата теста
        /// </summary>
        /// <param name="model">Модель результатов теста</param>
        /// <returns></returns>
        [HttpPost("test-result")]
        public async Task<IActionResult> TestResult([FromBody] TestResult model)
        {
            var result = await _courseService.TestResult(model);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        /// <summary>
        /// Сохранение результата урока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("lesson-sucsess")]
        public async Task<IActionResult> LessonResult([FromBody] LessonResult model)
        {
            await _courseService.LessonResult(model);
            return Ok();
        }

        /// <summary>
        /// Получение всех результатов тестов
        /// </summary>
        /// <returns></returns>
        [HttpGet("test-results")]
        public async Task<IActionResult> GetAllTestResults()
        {
            return Ok(await _courseService.GetAllTestResults()); 
        }

        /// <summary>
        /// Получение результатов тестов пользователя
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [HttpGet("test-result")]
        public async Task<IActionResult> GetTestResultsUser([FromQuery] long telegramId)
        {
            return Ok(await _courseService.GetTestResultsUser(telegramId));
        }

        /// <summary>
        /// Получение успешности теста
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="testId"></param>
        /// <returns></returns>
        [HttpGet("test-sucsess")]
        public async Task<IActionResult> GetTestSucsess([FromQuery] long telegramId, Guid testId)
        {
            return Ok(await _courseService.GetTestSucsess(telegramId, testId));
        }

        /// <summary>
        /// Получение успешности урока
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        [HttpGet("lesson-sucsess")]
        public async Task<IActionResult> GetLessonSucsess([FromQuery] long telegramId, [FromQuery] Guid lessonId)
        {
            return Ok(await _courseService.GetLessonSucsess(telegramId, lessonId));
        }

        /// <summary>
        /// Получить ответа пользователя на тест
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [HttpGet("replies-report")]
        public async Task<IActionResult> GetRepliesReport([FromQuery] long telegramId)
        {
            var repliesReport = await _courseService.GetRepliesReport(telegramId);

            return Ok(repliesReport);
        }

        /// <summary>
        /// Получить все ответы пользователей на тесты
        /// </summary>
        /// <returns></returns>
        [HttpGet("replies-reports")]
        public async Task<IActionResult> GetAllRepliesReports()
        {
            var repliesReports = await _courseService.GetAllRepliesReports();

            return Ok(repliesReports);
        }

        /// <summary>
        /// Создание отчета по ответам пользователей на тесты
        /// </summary>
        /// <param name="repliesReport"></param>
        /// <returns></returns>
        [HttpPost("create-replies-report")]
        public async Task<IActionResult> CreateRepliesReport([FromBody] RepliesReport repliesReport)
        {
            await _courseService.CreateRepliesReport(repliesReport);

            return Ok();
        }

        /// <summary>
        /// Получить список успешных уроков пользователя
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [HttpGet("lessons-sucsess")]
        public async Task<IActionResult> GetLessonsResult([FromQuery] long telegramId)
        {
            var result = await _courseService.GetLessonsSucsess(telegramId);

            return Ok(result);
        }

        /// <summary>
        /// Получить урок по его идентификатору
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        [HttpGet("lesson")]
        public async Task<IActionResult> GetLesson([FromQuery] Guid lessonId)
        {
            var lesson = await _courseService.GetLesson(lessonId);

            return Ok(lesson);
        }

        /// <summary>
        /// Получить список посещенных уроков
        /// </summary>
        /// <returns></returns>
        [HttpGet("visits")]
        public async Task<IActionResult> GetVisits()
        {
            var result = await _courseService.GetVisitsLessons();

            return Ok(result);
        }

        /// <summary>
        /// Создание нового посещения урока
        /// </summary>
        /// <param name="visitLesson"></param>
        /// <returns></returns>
        [HttpPost("visit")]
        public async Task<IActionResult> NewVisit([FromBody] VisitLesson visitLesson)
        {
            var result = await _courseService.NewVisitLesson(visitLesson);

            return Ok(result);
        }

        /// <summary>
        /// Отметить курс как завершенный
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [HttpPatch("finish-course")]
        public async Task<IActionResult> FinishCourse([FromQuery] Guid courseId, long telegramId)
        {
            var result = await _courseService.CourseSucsessUpdate(courseId, telegramId);

            return Ok(result);
        }

        /// <summary>
        /// Отметить блок как завершенный
        /// </summary>
        /// <param name="blockId"></param>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [HttpPatch("block-finish")]
        public async Task<IActionResult> FinishBlock([FromQuery] Guid blockId, long telegramId)
        {
            var result = await _courseService.BlockSucsessUpdate(blockId, telegramId);

            return Ok(result);
        }

        /// <summary>
        /// Отметить блок как посещенный
        /// </summary>
        /// <param name="visitBlock"></param>
        /// <returns></returns>
        [HttpPost("visit-block")]
        public async Task<IActionResult> VisitBlock([FromBody] VisitBlock visitBlock)
        {
            var result = await _courseService.VisitBlock(visitBlock);

            return Ok(result);
        }

        /// <summary>
        /// Получить список уроков по идентификатору блока
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        [HttpGet("lessons")]
        public async Task<IActionResult> GetLessonsByBlockId([FromQuery] Guid blockId)
        {
            var result = await _courseService.GetLessonsByBlockId(blockId);

            return Ok(result);
        }

        /// <summary>
        /// Получить курс по идентификатору блока
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        [HttpGet("course-by-block")]
        public async Task<IActionResult> GetCourseByBlockId([FromQuery] Guid blockId)
        {
            var result = await _courseService.GetCourseByBlockId(blockId);

            return Ok(result);
        }

        /// <summary>
        /// Получить тест по идентификатору блока
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        [HttpGet("test-block-id")]
        public async Task<IActionResult> GetTestByBlockId([FromQuery] Guid blockId)
        {
            var result = await _courseService.GetTestByBlockId(blockId);

            return Ok(result);
        }
    }

}
