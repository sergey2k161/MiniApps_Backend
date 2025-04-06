using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Repositories.Interfaces;

namespace MiniApps_Backend.Business.Services.Logic
{
    /// <summary>
    /// Бизнес логика пользователя
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Конструктор бизнес логики пользователя
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="userRequest">Модель пользователя</param>
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
                        UserName = userRequest.UserName
                    };

                    await _userRepository.AddUser(user);
                }

                await _userRepository.UpdateLevelUser(axitUser);
                return new ResultDto();
            }
            catch (Exception ex)
            {
                return new ResultDto(new List<string> { $"Обнаружена ошибка {ex}" });
            }
        }

        /// <summary>
        /// Получение пользователя по Id
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
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

        /// <summary>
        /// Получение пользователя по Telegram Id
        /// </summary>
        /// <param name="id">Telegram Id</param>
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
