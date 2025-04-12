using AutoMapper;
using MiniApps_Backend.Bot;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Dto.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity.ManyToMany;
using MiniApps_Backend.DataBase.Repositories.Interfaces;

namespace MiniApps_Backend.Business.Services.Logic
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courserRepository;
        private readonly IMapper _mapper;
        private readonly IBotService _botService;

        public CourseService(ICourseRepository courserRepository, IMapper mapper, IBotService botService)
        {
            _courserRepository = courserRepository;
            _mapper = mapper;
            _botService = botService;
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
            return await _courserRepository.GetCourseById(courseId);
        }

        /// <summary>
        /// Получение списка всех курсов
        /// </summary>
        /// <returns>Список курсов</returns>
        public async Task<List<Course>> GetCourses()
        {
            return await _courserRepository.GetCourses();
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
    }
}
