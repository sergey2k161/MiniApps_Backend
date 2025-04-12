using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Dto.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity.ManyToMany;
using MiniApps_Backend.DataBase.Repositories.Interfaces;


namespace MiniApps_Backend.DataBase.Repositories.DataAccess
{
    /// <summary>
    /// Репозиторий курса
    /// </summary>
    public class CourseRepository : ICourseRepository
    {
        private readonly MaDbContext _context;

        /// <summary>
        /// Конструктор репозитория курса
        /// </summary>
        /// <param name="context"></param>
        public CourseRepository(MaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавление связи материал - урок
        /// </summary>
        /// <param name="meterial">Сущность КурсМатериал</param>
        /// <returns></returns>>
        public async Task<ResultDto> AddMeterial(CourseMaterial meterial)
        {
            try
            {
                var material = await _context.CourseMaterials
                    .FirstOrDefaultAsync(c => c.TriggerKey == meterial.TriggerKey && c.TelegramMessageId == meterial.TelegramMessageId);

                if (material != null)
                {
                    return new ResultDto(new List<string> { $"Данный материал уже созан" });
                }

                await _context.AddAsync(meterial);
                await _context.SaveChangesAsync();

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }
        }

        /// <summary>
        /// Создание курса
        /// </summary>
        /// <param name="course">Сущность курса</param>
        /// <returns>Результат создания курса</returns>
        public async Task<ResultDto> CreateCourse(Course course)
        {
            //using var transaction = await _context.Database.BeginTransactionAsync();

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
                //await transaction.CommitAsync();

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }
        }

        /// <summary>
        /// Список всех результатов теста
        /// </summary>
        /// <returns>Список результатов</returns>
        public async Task<List<TestResult>> GetAllTestResults()
        {
            return await _context.TestResults.ToListAsync();
        }

        /// <summary>
        /// Получение списка материалов для урока
        /// </summary>
        /// <param name="triggerKey">Тригер слово</param>
        /// <returns>Список материалов</returns>
        public async Task<List<CourseMaterial>> GetByTriggerKeyAsync(string triggerKey)
        {
            try
            {
                return await _context.CourseMaterials
                    .Where(x => x.TriggerKey == triggerKey)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<CourseMaterial>();
            }
            
        }

        /// <summary>
        /// Получение курса по идентификатору 
        /// </summary>
        /// <param name="courseId">Идентификтор курса</param>
        /// <returns>Курс</returns>
        public async Task<Course> GetCourseById(Guid courseId)
        {
            return await _context.Courses
                .Include(c => c.Lessons)
                .ThenInclude(t => t.Test)
                .ThenInclude(q => q.Questions)
                .ThenInclude(a => a.Answers)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        /// <summary>
        /// Получение списка всех курсов
        /// </summary>
        /// <returns>Список курсов</returns>
        public async Task<List<Course>> GetCourses()
        {
            return await _context.Courses
                .Include(c => c.Lessons)
                .ThenInclude(t => t.Test)
                .ThenInclude(q => q.Questions)
                .ThenInclude(a => a.Answers)
                .ToListAsync();
        }

        /// <summary>
        /// Получение списка уроков в курсе
        /// </summary>
        /// <param name="courseId">Идентификтор курса</param>
        /// <returns></returns>
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

        /// <summary>
        /// Завершен ли урок у пользователя
        /// </summary>
        /// <param name="telegramId">Ид телеграмм</param>
        /// <param name="lessonId">Ид урока</param>
        /// <returns>Да НЕТ</returns>
        public async Task<bool> GetLessonSucsess(long telegramId, Guid lessonId)
        {
            var sucsess = await _context.LessonResults.FirstOrDefaultAsync(l => l.LessonId == lessonId && l.TelegramId == telegramId);

            return sucsess == null ? false : true;
        }

        /// <summary>
        /// Получение фопросов к тесту урока
        /// </summary>
        /// <param name="testId">Идентификтор теста</param>
        /// <returns>Список вопросов</returns>
        public async Task<List<Question>> GetQuestionsByTestId(Guid testId)
        {
            return await _context.Questions
                .Where(t => t.TestId == testId)
                .Include(t => t.Answers)
                .ToListAsync();
        }

        /// <summary>
        /// Результаты тестов пользователя
        /// </summary>
        /// <param name="telegramId">Ид телеграмм</param>
        /// <returns>Список результатов тестов пользователя</returns>
        public Task<List<TestResult>> GetTestResultsUser(long telegramId)
        {
            return _context.TestResults
                .Where(l => l.TelegramId == telegramId)
                .ToListAsync();
        }

        /// <summary>
        /// Завершен ли тест у пользователя
        /// </summary>
        /// <param name="telegramId">Ид телеграмм</param>
        /// <returns></returns>
        public async Task<TestResult> GetTestSucsess(long telegramId)
        {
            return await _context.TestResults.FirstOrDefaultAsync(t => t.TelegramId == telegramId && t.Result == true);
        }

        /// <summary>
        /// Добавление записи о завершенном уроке
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task<ResultDto> LessonResult(LessonResult result)
        {
            try
            {
                await _context.LessonResults.AddAsync(result);
                await _context.SaveChangesAsync();

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }
        }

        /// <summary>
        /// Подписка на курс
        /// </summary>
        /// <param name="subscription">Сущность подписки ну курс</param>
        /// <returns>Результат подпи</returns>
        public async Task<ResultDto> SubscribeToCourse(CourseSubscription subscription)
        {
            try
            {
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == subscription.CourseId);
                var price = course.Price;


                var isSubscribe = await UserIsSubscribe(subscription.TelegramId, subscription.CourseId);

                if (isSubscribe)
                {
                    return new ResultDto(new List<string> { $"Пользователь уже подписан на этот курс" });
                }

                await _context.CourseSubscriptions.AddAsync(subscription);
                await _context.SaveChangesAsync();

                return new ResultDto();

            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }
        }

        /// <summary>
        /// Добавление резултата теста
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task<ResultDto> TestResult(TestResult result)
        {
            try
            {
                await _context.TestResults.AddAsync(result);
                await _context.SaveChangesAsync();

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }
        }

        /// <summary>
        /// Проверка, подписан ли пользователь на курс
        /// </summary>
        /// <param name="telegramId">Идентификтор пользователя</param>
        /// <param name="courseId">Идентификтор курса</param>
        /// <returns></returns>
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
