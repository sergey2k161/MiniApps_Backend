using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity.ManyToMany;
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
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var testsToAdd = new List<Test>();
                foreach (var lesson in course.Lessons)
                {
                    if (lesson.Test != null)
                    {
                        testsToAdd.Add(lesson.Test);
                        lesson.Test = null;
                    }
                }

                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();

                for (int i = 0; i < course.Lessons.Count; i++)
                {
                    var lesson = course.Lessons[i];

                    if (i < testsToAdd.Count)
                    {
                        var test = testsToAdd[i];
                        test.LessonId = lesson.Id;
                        await _context.Tests.AddAsync(test);
                    }
                }

                for (int i = 0; i < course.Lessons.Count; i++)
                {
                    var lesson = course.Lessons[i];
                    var test = testsToAdd[i];

                    test.LessonId = lesson.Id;
                    await _context.Tests.AddAsync(test);

                    lesson.TestId = test.Id;
                }

                // Сохраняем все изменения
                await _context.SaveChangesAsync();


                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ResultDto();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
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

        public async Task<object> GetLessonsByCourseId(Guid courseId)
        {
            //var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);

            var lessons = await _context.Lessons
                .Where(l => l.CourseId == courseId)
                .Select(l => new
                {
                    l.Title,
                    l.Description,
                    l.UrlVideo,
                    l.Experience,
                    l.TestId
                })
                .ToListAsync();

            return lessons;
        }

        public async Task<List<Question>> GetQuestionsByTestId(Guid testId)
        {
            return await _context.Questions
                .Where(t => t.TestId == testId)
                .Include(t => t.Answers)
                .ToListAsync();
        }



        public async Task<ResultDto> SubscribeToCourse(CourseSubscription subscription)
        {
            await _context.CourseSubscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();

            return new ResultDto();
        }

        public async Task<bool> UserIsSubscribe(long telegramId, Guid courseId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);

            var sub = await _context.CourseSubscriptions
                .FirstOrDefaultAsync(u => u.TelegramId == telegramId && u.CourseId == courseId);

            if (sub == null)
            {
                return false;
            }

            return true;
        }
    }
}
