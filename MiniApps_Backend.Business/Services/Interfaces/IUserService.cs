using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;

namespace MiniApps_Backend.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByTelegramId(long telegramId);
        Task<User> GetUserById(Guid id);

        Task<ResultDto> AddUser(UserRequest userRequest);
    }
}
