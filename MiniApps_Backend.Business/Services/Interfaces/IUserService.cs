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
    }
}
