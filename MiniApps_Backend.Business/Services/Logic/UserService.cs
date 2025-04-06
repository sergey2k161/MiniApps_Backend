using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Repositories.Interfaces;

namespace MiniApps_Backend.Business.Services.Logic
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultDto> AddUser(UserRequest userRequest)
        {           
            try
            {
                var axitUser = await _userRepository.GetUserByTelegramId(userRequest.TelegramId);
                if (axitUser == null)
                {
                    var user = new User
                    {
                        TelegramId = userRequest.TelegramId,
                        FirstName = userRequest.FirstName,
                        LastName = userRequest.LastName,
                        UserName = userRequest.UserName,
                        Experience = 0
                    };

                    await _userRepository.AddUser(user);
                }

                return new ResultDto();
            }
            catch (Exception ex)
            {
                return new ResultDto(new List<string> { $"Обнаружена ошибка {ex}" });
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                return await _userRepository.GetUserById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in service {ex.Message}");
            }
        }

        public async Task<User> GetUserByTelegramId(long telegramId)
        {
            try
            {
                return await _userRepository.GetUserByTelegramId(telegramId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in service {ex.Message}");
            }
        }
    }
}
