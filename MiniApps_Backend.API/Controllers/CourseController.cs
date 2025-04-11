using Microsoft.AspNetCore.Mvc;
using MiniApps_Backend.Business.Services.Interfaces;
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

        [HttpGet("all")]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _courseService.GetCourses();

            return Ok(courses);
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseById(Guid courseid)
        {
            var course = await _courseService.GetCourseById(courseid);

            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CourseDto model)
        {
            await _courseService.CreateCourse(model);

            return Ok();
        }

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

        [HttpGet("isSubscribe")]
        public async Task<IActionResult> IsSubscribe([FromQuery] long telegramId, [FromQuery] Guid courseId)
        {
            var sub = await _courseService.UserIsSubscribe(telegramId, courseId);

            return Ok(sub);
        }

        [HttpGet("lessonByCourse")]
        public async Task<IActionResult> GetLessonsByCourseId([FromQuery] Guid courseId)
        {
            return Ok(await _courseService.GetLessonsByCourseId(courseId));
        }

        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestionsByTestId([FromQuery] Guid testId)
        {
            var q = await _courseService.GetQuestionsByTestId(testId);

            return Ok(q);
        }
    }
}
