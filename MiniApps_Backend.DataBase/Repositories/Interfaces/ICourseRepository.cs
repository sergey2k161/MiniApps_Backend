using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Dto.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity;
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
        Task<object> GetBlocksByCourseId(Guid courseId);

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

        /// <summary>
        /// Создание результата теста
        /// </summary>
        /// <param name="result">Сущность результата</param>
        /// <returns>Результат</returns>
        Task<ResultDto> TestResult(TestResult result);

        /// <summary>
        /// Содание результата прохождения урока
        /// </summary>
        /// <param name="result">Сущность результата</param>
        /// <returns>Результат</returns>
        Task<ResultDto> LessonResult(LessonResult result);

        /// <summary>
        /// Получить список результатов тестирования
        /// </summary>
        /// <returns>Список результатов</returns>
        Task<List<TestResult>> GetAllTestResults();

        /// <summary>
        /// Получить список результатов пользователя
        /// </summary>
        /// <param name="telegramId">идентификатор телеграм</param>
        /// <returns>Список результатов</returns>
        Task<List<TestResult>> GetTestResultsUser(long telegramId);

        /// <summary>
        /// Результат где тест пройден
        /// </summary>
        /// <param name="telegramId">идентификатор телеграм</param>
        /// <returns></returns>
        Task<bool> GetTestSucsess(long telegramId, Guid testId);

        /// <summary>
        /// Пройден ли урок
        /// </summary>
        /// <param name="telegramId">идентификатор телеграм</param>
        /// <returns></returns>
        Task<bool> GetLessonSucsess(long telegramId, Guid lessonId);

        /// <summary>
        /// Получить ответы пользователя на тест
        /// </summary>
        /// <param name="TelegramId">идентификатор телеграм</param>
        /// <returns>RepliesReport/null</returns>
        Task<RepliesReport> GetRepliesReport(long telegramId);

        /// <summary>
        /// Создать запись RepliesReport
        /// </summary>
        /// <param name="repliesReport"></param>
        /// <returns></returns>
        Task<ResultDto> CreateRepliesReport(RepliesReport repliesReport);

        /// <summary>
        /// Получить список RepliesReport
        /// </summary>
        /// <returns></returns>
        Task<List<RepliesReport>> GetAllRepliesReports();

        Task<List<LessonResult>> GetLessonsSucsess(long telegramId);

        Task<Lesson> GetLesson(Guid lessonId);
    }
}
