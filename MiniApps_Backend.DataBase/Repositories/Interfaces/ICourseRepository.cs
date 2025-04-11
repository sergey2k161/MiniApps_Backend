using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity.ManyToMany;
using MiniApps_Backend.DataBase.Repositories.DataAccess;

namespace MiniApps_Backend.DataBase.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<ResultDto> CreateCourse(Course course);

        Task<List<Course>> GetCourses();

        Task<Course> GetCourseById(Guid courseId);

        Task<ResultDto> SubscribeToCourse(CourseSubscription subscription);

        Task<bool> UserIsSubscribe(long telegramId, Guid courseId);

        Task<object> GetLessonsByCourseId(Guid courseId);

        //Task<object> GetTestByLessonId(Guid lessonId);

        Task<List<Question>> GetQuestionsByTestId(Guid testId);
    }
}
