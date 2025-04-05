using MiniApps_Backend.Business.Services.Interfaces;
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

        public async Task AddUser(User user, long telegramId)
        {
            try
            {
                var axistUser = await _userRepository.GetUserByTelegramId(telegramId);

                if (axistUser == null)
                {
                    var newUser = new User
                    {
                        TelegramId = telegramId,
                        Experience = 0
                    };

                    await _userRepository.AddUser(newUser);
                } 
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in service {ex.Message}");
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
