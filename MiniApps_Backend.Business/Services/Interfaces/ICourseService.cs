using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Dto.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;

namespace MiniApps_Backend.Business.Services.Interfaces
{
    public interface ICourseService
    {
        Task<ResultDto> CreateCourse(CourseDto model);

        Task<List<Course>> GetCourses();

        Task<Course> GetCourseById(Guid courseId);

        Task<ResultDto> SubscribeToCourse(Guid courseId, long telegramId);

        Task<bool> UserIsSubscribe(long telegramId, Guid courseId);

        Task<object> GetLessonsByCourseId(Guid courseId);

        Task<List<Question>> GetQuestionsByTestId(Guid testId);
    }
}
