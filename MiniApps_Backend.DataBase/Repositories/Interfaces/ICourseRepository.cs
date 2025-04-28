using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Dto.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.Analysis;
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
        Task<List<Block>> GetBlocksByCourseId(Guid courseId);

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
        Task<List<TestResultDto>> GetAllTestResults();

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

        /// <summary>
        /// Получить список успешных уроков пользователя
        /// </summary>
        /// <param name="telegramId">Идентификатор пользователя в Telegram</param>
        /// <returns>Список успешных уроков</returns>
        Task<List<LessonResult>> GetLessonsSucsess(long telegramId);

        /// <summary>
        /// Получить информацию об уроке
        /// </summary>
        /// <param name="lessonId">Идентификатор урока</param>
        /// <returns>Урок</returns>
        Task<Lesson> GetLesson(Guid lessonId);

        /// <summary>
        /// Получить последний результат теста пользователя
        /// </summary>
        /// <param name="testId">Идентификатор теста</param>
        /// <param name="telegramId">Идентификатор пользователя в Telegram</param>
        /// <returns>Результат теста</returns>
        Task<TestResult> GetLastTestResult(Guid testId, long telegramId);

        /// <summary>
        /// Создать запись о посещении урока
        /// </summary>
        /// <param name="visitLesson">Сущность посещения урока</param>
        /// <returns>Результат операции</returns>
        Task<ResultDto> NewVisitLesson(VisitLesson visitLesson);

        /// <summary>
        /// Получить список посещений уроков
        /// </summary>
        /// <returns>Список посещений уроков</returns>
        Task<List<VisitLesson>> GetVisitsLessons();

        /// <summary>
        /// Создать запись об успешном завершении курса
        /// </summary>
        /// <param name="dto">DTO успешного завершения курса</param>
        /// <returns>Результат операции</returns>
        Task<ResultDto> CourseSucsess(CourseSucsessDto dto);

        /// <summary>
        /// Обновить запись об успешном завершении курса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="telegramId">Идентификатор пользователя в Telegram</param>
        /// <returns>Результат операции</returns>
        Task<ResultDto> CourseSucsessUpdate(Guid courseId, long telegramId);

        /// <summary>
        /// Создать запись об успешном завершении блока
        /// </summary>
        /// <param name="dto">DTO успешного завершения блока</param>
        /// <returns>Результат операции</returns>
        Task<ResultDto> BlockSucsess(BlockSucsessDto dto);

        /// <summary>
        /// Обновить запись об успешном завершении блока
        /// </summary>
        /// <param name="blockId">Идентификатор блока</param>
        /// <param name="telegramId">Идентификатор пользователя в Telegram</param>
        /// <returns>Результат операции</returns>
        Task<ResultDto> BlockSucsessUpdate(Guid blockId, long telegramId);

        /// <summary>
        /// Создать запись о посещении блока
        /// </summary>
        /// <param name="visitBlock">Сущность посещения блока</param>
        /// <returns>Результат операции</returns>
        Task<ResultDto> VisitBlock(VisitBlock visitBlock);

        /// <summary>
        /// Получить список посещений блоков
        /// </summary>
        /// <returns>Список посещений блоков</returns>
        Task<List<VisitBlock>> GetVisitsBlocks();

        /// <summary>
        /// Получить информацию о блоке
        /// </summary>
        /// <param name="blockId">Идентификатор блока</param>
        /// <returns>Блок</returns>
        Task<Block> GetBlock(Guid blockId);

        /// <summary>
        /// Получить список успешных завершений блоков
        /// </summary>
        /// <returns>Список успешных завершений блоков</returns>
        Task<List<BlockSucsessDto>> GetBlockSucsess();

        /// <summary>
        /// Рассчитать процент завершения курса
        /// </summary>
        /// <param name="telegramId">Идентификатор пользователя в Telegram</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Процент завершения курса</returns>
        Task<double> PercentageCompletionCourse(long telegramId, Guid courseId);

        /// <summary>
        /// Рассчитать процент завершения блока
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Процент завершения блока</returns>
        Task<double> PercentageCompletionBlock(Guid courseId);

        /// <summary>
        /// Рассчитать процент отсева блока
        /// </summary>
        /// <param name="blockId">Идентификатор блока</param>
        /// <returns>Процент отсева блока</returns>
        Task<double> PercentageDropoutBlock(Guid blockId);

        /// <summary>
        /// Получить список уроков по идентификатору блока
        /// </summary>
        /// <param name="blockId">Идентификатор блока</param>
        /// <returns>Список уроков</returns>
        Task<List<Lesson>> GetLessonsByBlockId(Guid blockId);

        /// <summary>
        /// Получить курс по идентификатору блока
        /// </summary>
        /// <param name="blockId">Идентификатор блока</param>
        /// <returns>Курс</returns>
        Task<Course> GetCourseByBlockId(Guid blockId);

        /// <summary>
        /// Получить тест по идентификатору блока
        /// </summary>
        /// <param name="blockId">Идентификатор блока</param>
        /// <returns>Тест</returns>
        Task<Test> GetTestByBlockId(Guid blockId);

        Task<ResultDto> CourseUpdate(Guid courseId, CourseDto courseModel);
    }
}
