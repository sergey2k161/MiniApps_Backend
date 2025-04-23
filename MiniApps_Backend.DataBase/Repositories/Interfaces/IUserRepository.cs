using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;

namespace MiniApps_Backend.DataBase.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория пользователя
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Получение пользователя по Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<User> GetUserByTelegramId(long telegramId);

        /// <summary>
        /// Получение пользователя по Telegram Id
        /// </summary>
        /// <param name="telegramId">Telegram Id</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<User> GetUserById(Guid id);

        /// <summary>
        /// Добавление нового пользователя в БД
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        /// <returns></returns>
        Task<ResultDto> AddUser(User user);

        /// <summary>
        /// Обновление уровня пользователя, в зависимости от опыта
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        Task<ResultDto> UpdateLevelUser(User user);

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="commonUserId">Идентификтор с таблицы CommonUser</param>
        /// <param name="walletId">Идентификтор кошелька</param>
        /// <returns>Результат обновления данных</returns>
        Task<ResultDto> UpdateUserAsync(User user, Guid commonUserId, Guid walletId);

        /// <summary>
        /// Список идентификаторов курсов, на которые подписан пользователь
        /// </summary>
        /// <param name="telegramId">Идентификтор пользователя</param>
        /// <returns>Список Идентификторов</returns>
        Task<List<Guid>> GetSubscribesList(long telegramId);

        /// <summary>
        /// Получить список пользователей
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetAllUsers();

        /// <summary>
        /// Список пользователей, которым можно отправлять напоминания
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetUsersForNotification();

        /// <summary>
        /// Выключение/Выключение уведомлений
        /// </summary>
        /// <param name="telegramId">Идентификтор пользователя</param>
        /// <returns></returns>
        Task<ResultDto> NotificationSwitch(long telegramId);

        /// <summary>
        /// Изменить частоту уведомлений
        /// </summary>
        /// <param name="telegramId">Идентификтор пользователя</param>
        /// <returns></returns>
        Task<ResultDto> ChangeNotificationFrequency(long telegramId, int frequency);

        /// <summary>
        /// Обновить данные пользователя
        /// </summary>
        /// <param name="telegramId">Идентификтор пользователя</param>
        /// <returns></returns>
        Task<ResultDto> UpdateUser(UserUpdateDto model);

        /// <summary>
        /// Наличие активного курса
        /// </summary>
        /// <param name="telegramId">Идентификтор пользователя</param>
        /// <returns></returns>
        Task<ResultDto> SwitchActiveCourse(long telegramId);

        Task<ResultDto> UpdateExpForUser(long telegramId, int exp);

        Task<List<User>> GetUsers();

        Task<bool> GetActiveCourses(long telegramId);

        Task<bool> GetActiveBlockForCourse(long telegramId, Guid blockId);
    }
}
