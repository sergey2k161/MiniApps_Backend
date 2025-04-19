using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MiniApps_Backend.Bot;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Dto.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity.ManyToMany;
using MiniApps_Backend.DataBase.Repositories.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace MiniApps_Backend.Business.Services.Logic
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courserRepository;
        private readonly IMapper _mapper;
        private readonly IBotService _botService;
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletService _walletService;
        private readonly IDistributedCache _cache;
        private readonly ILogger<CourseService> _logger;



        public CourseService(ICourseRepository courserRepository, IMapper mapper, IBotService botService, IWalletRepository walletRepository, IWalletService walletService, IDistributedCache cache, ILogger<CourseService> logger)
        {
            _courserRepository = courserRepository;
            _mapper = mapper;
            _botService = botService;
            _walletRepository = walletRepository;
            _walletService = walletService;
            _cache = cache;
            _logger = logger;
        }

        /// <summary>
        /// Создание курса
        /// </summary>
        /// <param name="course">Модель ДТО курса</param>
        /// <returns>Результат создания</returns>
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

            const string coursesCacheKey = "courses_cache";
            var courses = await _courserRepository.GetCourses();

            var serializedCourses = JsonConvert.SerializeObject(courses, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });

            await _cache.SetAsync(
                coursesCacheKey,
                Encoding.UTF8.GetBytes(serializedCourses),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(365)
                });

            return new ResultDto();
        }

        /// <summary>
        /// Отправка сообщений пользователю в чат
        /// </summary>
        /// <param name="triggerKey">Триггер фраза</param>
        /// <param name="userChatId">Ид чата</param>
        /// <returns></returns>
        public async Task SendMaterialsByTriggerAsync(string triggerKey, long userChatId)
        {
            var materials = await _courserRepository.GetByTriggerKeyAsync(triggerKey);

            foreach (var material in materials)
            {
                await _botService.ForwardMessageAsync(userChatId, material.TelegramChatId, material.TelegramMessageId);
            }
        }

        /// <summary>
        /// Получение курса по идентификатору 
        /// </summary>
        /// <param name="courseId">Идентификтор курса</param>
        /// <returns>Курс</returns>
        public async Task<Course> GetCourseById(Guid courseId)
        {
            var courseCache = $"course_cache_{courseId}";

            var cachedCourse = await _cache.GetAsync(courseCache);

            if (cachedCourse != null)
            {
                var cachedCourseString = Encoding.UTF8.GetString(cachedCourse);
                _logger.LogInformation("Курс получен из кэша");

                return JsonConvert.DeserializeObject<Course>(cachedCourseString);
            }

            var course = await _courserRepository.GetCourseById(courseId);
            _logger.LogInformation("Курс получен из базы и добавлен в кэш");

            var serializedCourse = JsonConvert.SerializeObject(course, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });

            await _cache.SetAsync(
                courseCache,
                Encoding.UTF8.GetBytes(serializedCourse),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(365)
                });

            return course;
        }

        /// <summary>
        /// Получение списка всех курсов
        /// </summary>
        /// <returns>Список курсов</returns>
        public async Task<List<Course>> GetCourses()
        {
            const string coursesCacheKey = "courses_cache";

            // 1. Попытка получить из кэша
            var cachedCourses = await _cache.GetAsync(coursesCacheKey);

            if (cachedCourses != null)
            {
                var cachedCoursesString = Encoding.UTF8.GetString(cachedCourses);
                _logger.LogInformation("Курсы получены из кэша");

                return JsonConvert.DeserializeObject<List<Course>>(cachedCoursesString);
            }

            var courses = await _courserRepository.GetCourses();
            _logger.LogInformation("Курсы получены из базы и добавлены в кэш");

            var serializedCourses = JsonConvert.SerializeObject(courses, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });

            await _cache.SetAsync(
                coursesCacheKey,
                Encoding.UTF8.GetBytes(serializedCourses),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(365)
                });

            return courses;
        }

        /// <summary>
        /// Получение списка уроков в курсе
        /// </summary>
        /// <param name="courseId">Идентификтор курса</param>
        /// <returns></returns>
        public async Task<object> GetLessonsByCourseId(Guid courseId)
        {
            return await _courserRepository.GetLessonsByCourseId(courseId);
        }

        /// <summary>
        /// Получение вопросов к тесту урока
        /// </summary>
        /// <param name="testId">Идентификтор теста</param>
        /// <returns>Список вопросов</returns>
        public async Task<List<Question>> GetQuestionsByTestId(Guid testId)
        {
            return await _courserRepository.GetQuestionsByTestId(testId);
        }

        /// <summary>
        /// Подписка на курс
        /// </summary>
        /// <param name="courseId">Ид курса</param>
        /// <param name="telegramId">Ид пользователя</param>
        /// <returns>Результат подписки</returns>
        public async Task<ResultDto> SubscribeToCourse(Guid courseId, long telegramId)
        {
            var isSubscribe = await UserIsSubscribe(telegramId, courseId);

            if (isSubscribe)
            {
                return new ResultDto(new List<string> { "Пользователь уже подписан на этот курс" });
            }

            var course = await _courserRepository.GetCourseById(courseId);
            decimal price;

            if (course.Discount && course.PriceWithDiscount.HasValue)
            {
                price = course.PriceWithDiscount.Value;
            }
            else
            {
                price = course.Price;
            }

            var balance = await _walletRepository.GetBalance(telegramId);

            if (balance - price < 0)
            {
                return new ResultDto(new List<string> { "недостаточно средств" });
            }

            //await _walletRepository.UpdateBalanse(telegramId, price);
            await _walletService.CreateTransaction(telegramId, false, false, 0, price);

            var subscription = new CourseSubscription
            {
                CourseId = courseId,
                TelegramId = telegramId
            };

            await _courserRepository.SubscribeToCourse(subscription);

            return new ResultDto();
        }

        /// <summary>
        /// Проверка, подписан ли пользователь на курс
        /// </summary>
        /// <param name="telegramId">Идентификтор пользователя</param>
        /// <param name="courseId">Идентификтор курса</param>
        /// <returns></returns>
        public async Task<bool> UserIsSubscribe(long telegramId, Guid courseId)
        {
            return await _courserRepository.UserIsSubscribe(telegramId, courseId);
        }

        /// <summary>
        /// Добавление связи материал - урок
        /// </summary>
        /// <param name="meterial">Сущность КурсМатериал</param>
        /// <returns></returns>
        public async Task AddMeterial(CourseMaterial meterial)
        {
            await _courserRepository.AddMeterial(meterial);
        }

        /// <summary>
        /// Добавление резултата теста
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task<ResultDto> TestResult(TestResult result)
        {
            var resultTest = new TestResult
            {
                TelegramId = result.TelegramId,
                TestId = result.TestId,
                LastTry = DateTime.UtcNow,
                Result = result.Result
            };

            await _courserRepository.TestResult(resultTest);

            return new ResultDto();
        }

        /// <summary>
        /// Завершен ли урок у пользователя
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task<ResultDto> LessonResult(LessonResult result)
        {
            var resultLesson = new LessonResult
            {
                TelegramId = result.TelegramId,
                LessonId = result.LessonId,
                WhenСompleted = DateTime.UtcNow
            };

            await _courserRepository.LessonResult(resultLesson);

            return new ResultDto();
        }

        /// <summary>
        /// Список всех результатов теста
        /// </summary>
        /// <returns>Список результатов</returns>
        public async Task<List<TestResult>> GetAllTestResults()
        {
            return await _courserRepository.GetAllTestResults();
        }

        /// <summary>
        /// Результаты тестов пользователя
        /// </summary>
        /// <param name="telegramId">Ид телеграмм</param>
        /// <returns>Список результатов тестов пользователя</returns>
        public async Task<List<TestResult>> GetTestResultsUser(long telegramId)
        {
            return await _courserRepository.GetTestResultsUser(telegramId);
        }

        /// <summary>
        /// Завершен ли тест у пользователя
        /// </summary>
        /// <param name="telegramId">Ид телеграмм</param>
        /// <returns></returns>
        public async Task<TestResult> GetTestSucsess(long telegramId)
        {
            return await _courserRepository.GetTestSucsess(telegramId);
        }

        /// <summary>
        /// Завершен ли урок у пользователя
        /// </summary>
        /// <param name="telegramId">Ид телеграмм</param>
        /// <param name="lessonId">Ид урока</param>
        /// <returns>Да НЕТ</returns>
        public async Task<bool> GetLessonSucsess(long telegramId, Guid lessonId)
        {
            return await _courserRepository.GetLessonSucsess(telegramId, lessonId);
        }

        public async Task<RepliesReport> GetRepliesReport(long telegramId)
        {
            return await _courserRepository.GetRepliesReport(telegramId);
        }

        public async Task<ResultDto> CreateRepliesReport(RepliesReport repliesReport)
        {
            try
            {
                var newRepliesReport = new RepliesReport
                {
                    TelegramId = repliesReport.TelegramId,
                    QuestionId = repliesReport.QuestionId,
                    Answer = repliesReport.Answer,
                    TestId = repliesReport.TestId
                };

                await _courserRepository.CreateRepliesReport(newRepliesReport);

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БС" });
            }
        }

        public async Task<List<RepliesReport>> GetAllRepliesReports()
        {
            return await _courserRepository.GetAllRepliesReports();
        }
    }
}
