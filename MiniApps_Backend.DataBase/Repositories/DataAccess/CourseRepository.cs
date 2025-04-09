using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;
using MiniApps_Backend.DataBase.Repositories.Interfaces;

namespace MiniApps_Backend.DataBase.Repositories.DataAccess
{
    public class CourseRepository : ICourseRepository
    {
        private readonly MaDbContext _context;

        public CourseRepository(MaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> CreateCourse(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            return new ResultDto();
        }

        public async Task<Course> GetCourseById(Guid courseId)
        {
            return await _context.Courses
                .Include(c => c.Lessons)
                .ThenInclude(t => t.Test)
                .ThenInclude(q => q.Questions)
                .ThenInclude(a => a.Answers)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        public async Task<List<Course>> GetCourses()
        {
            return await _context.Courses
                .Include(c => c.Lessons)
                .ThenInclude(t => t.Test)
                .ThenInclude(q => q.Questions)
                .ThenInclude(a => a.Answers)
                .ToListAsync();
        }
    }
}
