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
            try
            {
                // Времено удаляем тесты из блоков курса
                var testsToAdd = new List<Test>();
                foreach (var block in course.Blocks)
                {
                    if (block.Test != null)
                    {
                        testsToAdd.Add(block.Test);
                        block.Test = null;
                    }
                }

                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();

                // Восстанавливаем тесты в блоках курса
                for (int i = 0; i < course.Blocks.Count; i++)
                {
                    if (i < testsToAdd.Count)
                    {
                        var test = testsToAdd[i];
                        var block = course.Blocks[i];

                        test.BlockId = block.Id;
                        block.TestId = test.Id;

                        await _context.Tests.AddAsync(test);
                    }
                }

                await _context.SaveChangesAsync();
                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }
        }

        /// <summary>
        /// Создать запись RepliesReport
        /// </summary>
        /// <param name="repliesReport"></param>
        /// <returns></returns>
        public async Task<ResultDto> CreateRepliesReport(RepliesReport repliesReport)
        {
            try
            {
                await _context.AddAsync(repliesReport);
                await _context.SaveChangesAsync();

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }
        }

        /// <summary>
        /// Получить список RepliesReport
        /// </summary>
        /// <returns></returns>
        public async Task<List<RepliesReport>> GetAllRepliesReports()
        {
            return await _context.RepliesReports.ToListAsync();
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
                .Include(c => c.Blocks)
                .ThenInclude(t => t.Test)
                .ThenInclude(q => q.Questions)
                .ThenInclude(a => a.Answers)
                //.Include(l => l.Lessons)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        /// <summary>
        /// Получение списка всех курсов
        /// </summary>
        /// <returns>Список курсов</returns>
        public async Task<List<Course>> GetCourses()
        {
            return await _context.Courses
                .Include(c => c.Blocks)
                .ThenInclude(t => t.Test)
                .ThenInclude(q => q.Questions)
                .ThenInclude(a => a.Answers)
                .ToListAsync();
        }

        public async Task<Lesson> GetLesson(Guid lessonId)
        {
            return await _context.Lessons.FirstOrDefaultAsync(l => l.Id == lessonId);
        }

        /// <summary>
        /// Получение списка уроков в курсе
        /// </summary>
        /// <param name="courseId">Идентификтор курса</param>
        /// <returns></returns>
        public async Task<object> GetBlocksByCourseId(Guid courseId)
        {
            var lessons = await _context.Blocks
                .Where(l => l.CourseId == courseId)
                .Select(l => new
                {
                    l.Id,
                    l.Title,
                    l.TestId
                })
                .ToListAsync();

            return lessons;
        }

        public async Task<List<LessonResult>> GetLessonsSucsess(long telegramId)
        {
            return await _context.LessonResults
                .Where(l => l.TelegramId == telegramId)
                .ToListAsync();
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
        /// Получить ответы пользователя на тест
        /// </summary>
        /// <param name="TelegramId">идентификатор телеграм</param>
        /// <returns>RepliesReport/null</returns>
        public async Task<RepliesReport> GetRepliesReport(long telegramId)
        {
            return await _context.RepliesReports.FirstOrDefaultAsync(t => t.TelegramId ==  telegramId);
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
        public async Task<bool> GetTestSucsess(long telegramId, Guid testId)
        {
            var testResult = await _context.TestResults.FirstOrDefaultAsync(t => t.TelegramId == telegramId && t.Result == true && t.TestId == testId);

            return testResult == null ? false : true;
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
