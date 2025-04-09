using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;

namespace MiniApps_Backend.DataBase.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<ResultDto> CreateCourse(Course course);

        Task<List<Course>> GetCourses();

        Task<Course> GetCourseById(Guid courseId);
    }
}
