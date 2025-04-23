using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;

namespace MiniApps_Backend.Business.Services.Interfaces
{
    /// <summary>
    /// Интефейс сервиса пользователя
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получение пользователя по Telegram Id
        /// </summary>
        /// <param name="id">Telegram Id</param>
        Task<User> GetUserByTelegramId(long telegramId);

        /// <summary>
        /// Получение пользователя по Id
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        Task<User> GetUserById(Guid id);

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="userRequest">Модель пользователя</param>
        Task<ResultDto> AddUser(UserRequest userRequest);

        /// <summary>
        /// Список идентификаторов курсов, на которые подписан пользователь
        /// </summary>
        /// <param name="telegramId">Идентификтор пользователя</param>
        /// <returns>Список Идентификторов</returns>
        Task<List<Guid>> GetSubscribesList(long telegramId);

        /// <summary>
        /// Получение роли пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <returns></returns>
        Task<List<string>> GetUserRoles(Guid userId);

        /// <summary>
        /// Изменить роль пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<ResultDto> ChangeUserRole(Guid userId, string roleName);

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

        Task<List<User>> GetUsers();

        Task<bool> GetActiveBlockForCourse(long telegramId, Guid blockId);

        Task<string?> GenerateAdminKey(long telegramId);
    }
}
