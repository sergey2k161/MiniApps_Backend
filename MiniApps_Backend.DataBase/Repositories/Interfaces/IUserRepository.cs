using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;

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

        Task<ResultDto> UpdateUserAsync(User user, Guid commonUserId);
    }
}
