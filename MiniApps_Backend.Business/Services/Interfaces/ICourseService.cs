using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;

namespace MiniApps_Backend.Business.Services.Interfaces
{
    public interface ICourseService
    {
        Task<ResultDto> CreateCourse(CourseDto model);

        Task<List<Course>> GetCourses();

        Task<Course> GetCourseById(Guid courseId);
    }
}
