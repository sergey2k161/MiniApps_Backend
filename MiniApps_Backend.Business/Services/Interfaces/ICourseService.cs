using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Dto.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;

namespace MiniApps_Backend.Business.Services.Interfaces
{
    /// <summary>
    /// Интерфейс для сервиса курса
    /// </summary>
    public interface ICourseService
    {
        /// <summary>
        /// Создание курса
        /// </summary>
        /// <param name="course">Модель ДТО курса</param>
        /// <returns>Результат создания</returns>
        Task<ResultDto> CreateCourse(CourseDto model);

        /// <summary>
        /// Получение списка всех курсов
        /// </summary>
        /// <returns>Список курсов</returns>
        Task<List<Course>> GetCourses();

        /// <summary>
        /// Получение курса по идентификатору 
        /// </summary>
        /// <param name="courseId">Идентификтор курса</param>
        /// <returns>Курс</returns>
        Task<Course> GetCourseById(Guid courseId);

        /// <summary>
        /// Подписка на курс
        /// </summary>
        /// <param name="courseId">Ид курса</param>
        /// <param name="telegramId">Ид пользователя</param>
        /// <returns>Результат подписки</returns>
        Task<ResultDto> SubscribeToCourse(Guid courseId, long telegramId);

        /// <summary>
        /// Проверка, подписан ли пользователь на курс
        /// </summary>
        /// <param name="telegramId">Идентификтор пользователя</param>
        /// <param name="courseId">Идентификтор курса</param>
        /// <returns></returns>
        Task<bool> UserIsSubscribe(long telegramId, Guid courseId);

        /// <summary>
        /// Получение списка уроков в курсе
        /// </summary>
        /// <param name="courseId">Идентификтор курса</param>
        /// <returns></returns>
        Task<object> GetLessonsByCourseId(Guid courseId);

        /// <summary>
        /// Получение вопросов к тесту урока
        /// </summary>
        /// <param name="testId">Идентификтор теста</param>
        /// <returns>Список вопросов</returns>
        Task<List<Question>> GetQuestionsByTestId(Guid testId);

        /// <summary>
        /// Отправка сообщений пользователю в чат
        /// </summary>
        /// <param name="triggerKey">Триггер фраза</param>
        /// <param name="userChatId">Ид чата</param>
        /// <returns></returns>
        Task SendMaterialsByTriggerAsync(string triggerKey, long userChatId);

        /// <summary>
        /// Добавление связи материал - урок
        /// </summary>
        /// <param name="meterial">Сущность КурсМатериал</param>
        /// <returns></returns>
        Task AddMeterial(CourseMaterial meterial);
    }
}
