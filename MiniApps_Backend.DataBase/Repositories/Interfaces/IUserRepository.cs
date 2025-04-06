using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;

namespace MiniApps_Backend.DataBase.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByTelegramId(long telegramId);
        Task<User> GetUserById(Guid id);

        Task<ResultDto> AddUser(User user);
    }
}
