using AutoMapper;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;
using MiniApps_Backend.DataBase.Repositories.Interfaces;

namespace MiniApps_Backend.Business.Services.Logic
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courserRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courserRepository, IMapper mapper)
        {
            _courserRepository = courserRepository;
            _mapper = mapper;
        }

        public async Task<ResultDto> CreateCourse(CourseDto model)
        {
            var exp = 0;
            var course = _mapper.Map<Course>(model);
            course.CreateAt = DateTime.UtcNow;

            foreach (var lesson in course.Lessons)
            {
                exp += lesson.Experience;
            }

            course.Experience = exp;

            await _courserRepository.CreateCourse(course);

            return new ResultDto();
        }

        public async Task<Course> GetCourseById(Guid courseId)
        {
            return await _courserRepository.GetCourseById(courseId);
        }

        public async Task<List<Course>> GetCourses()
        {
            return await _courserRepository.GetCourses();
        }
    }
}
