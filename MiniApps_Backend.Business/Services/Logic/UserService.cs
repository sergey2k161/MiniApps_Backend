using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MiniApps_Backend.Business.Services.Logic
{
    /// <summary>
    /// Бизнес логика пользователя
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<CommonUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;


        /// <summary>
        /// Конструктор бизнес логики пользователя
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService(IUserRepository userRepository, UserManager<CommonUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
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
                        UserName = userRequest.UserName,
                        Email = userRequest.Email
                    };

                    await _userRepository.AddUser(user);
                    axitUser = user;

                    var commonUser = new CommonUser
                    {
                        Email = userRequest.Email,
                        UserName = userRequest.Email,
                        UserId = user.Id
                    };
                    var result = await _userManager.CreateAsync(commonUser);

                    if (!result.Succeeded)
                    {
                        return new ResultDto { IsSuccess = false, Errors = result.Errors.Select(e => e.Description).ToList() };
                    }

                    await _userRepository.UpdateUserAsync(user, commonUser.Id);

                    var roleResult = await _userManager.AddToRoleAsync(commonUser, "User");

                    if (!roleResult.Succeeded)
                    {
                        return new ResultDto { IsSuccess = false, Errors = roleResult.Errors.Select(e => e.Description).ToList() };
                    }
                }



               // await _userRepository.UpdateLevelUser(axitUser);
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
                var user = await _userRepository.GetUserByTelegramId(telegramId);

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in service {ex.Message}");
            }
        }
    }
}
