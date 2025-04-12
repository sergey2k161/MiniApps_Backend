using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Dto.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity.ManyToMany;

namespace MiniApps_Backend.DataBase.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория курса
    /// </summary>
    public interface ICourseRepository
    {
        /// <summary>
        /// Создание курса
        /// </summary>
        /// <param name="course">Сущность курс</param>
        /// <returns>Результат создания</returns>
        Task<ResultDto> CreateCourse(Course course);

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
        /// <param name="subscription">Сущность подписки ну курс</param>
        /// <returns>Результат подпи</returns>
        Task<ResultDto> SubscribeToCourse(CourseSubscription subscription);

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
        /// Получение списка материалов по тригеру
        /// </summary>
        /// <param name="triggerKey">тригер слово</param>
        /// <returns></returns>
        Task<List<CourseMaterial>> GetByTriggerKeyAsync(string triggerKey);

        /// <summary>
        /// Добавление связи материал - урок
        /// </summary>
        /// <param name="meterial">Сущность КурсМатериал</param>
        /// <returns></returns>
        Task<ResultDto> AddMeterial(CourseMaterial meterial);
    }
}
