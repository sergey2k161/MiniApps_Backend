using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

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
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletService _walletService;


        /// <summary>
        /// Конструктор бизнес логики пользователя
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService(IUserRepository userRepository, UserManager<CommonUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IWalletRepository walletRepository, IWalletService walletService)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _walletRepository = walletRepository;
            _walletService = walletService;
        }

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="userRequest">Модель пользователя</param>
        public async Task<ResultDto> AddUser(UserRequest userRequest)
        {           
            try
            {
                // Создание пользователя
                var user = await CreateUser(userRequest);

                // Создание CommonUser
                var commonUser = new CommonUser
                {
                    Email = userRequest.Email,
                    UserName = userRequest.Email,
                    UserId = user.Id
                };
                var result = await _userManager.CreateAsync(commonUser);

                if (user.TelegramId == 631765973)
                {
                    var roleResult = await _userManager.AddToRoleAsync(commonUser, "Admin");
                }
                else
                {
                    var roleResult = await _userManager.AddToRoleAsync(commonUser, "User");
                }

                // Создание счета
                var newWallet = await _walletService.CreateWallet(user);
                await _userRepository.UpdateUserAsync(user, commonUser.Id, newWallet.Id);

                return new ResultDto();
            }
            catch (Exception ex)
            {
                return new ResultDto(new List<string> { $"Обнаружена ошибка {ex}" });
            }
        }

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        public async Task<User> CreateUser(UserRequest userRequest)
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
                    Email = userRequest.Email,
                    Phone = userRequest.Phone,
                    RealFirstName = userRequest.RealFirstName,
                    RealLastName = userRequest.RealLastName,
                    LastVisit = DateTime.UtcNow,
                    NotificationFrequency = 3,
                    TurnNotification = true,
                    LastNotification = DateTime.UtcNow,
                    ActiveCourse = false
                };

                await _userRepository.AddUser(user);

                return user;
            }

            return null;
        }

        /// <summary>
        /// Список идентификаторов курсов, на которые подписан пользователь
        /// </summary>
        /// <param name="telegramId">Идентификтор пользователя</param>
        /// <returns>Список Идентификторов</returns>
        public async Task<List<Guid>> GetSubscribesList(long telegramId)
        {
            var subs =  await _userRepository.GetSubscribesList(telegramId);

            return subs;
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
        /// Список идентификаторов курсов, на которые подписан пользователь
        /// </summary>
        /// <param name="telegramId">Идентификтор пользователя</param>
        /// <returns>Список Идентификторов</returns>
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

        /// <summary>
        /// Получение роли пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <returns>Список ролей</returns>
        public async Task<List<string>> GetUserRoles(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }

            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        /// <summary>
        /// Изменение роли пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="roleName">Название новой роли</param>
        /// <returns>Результат операции</returns>
        public async Task<ResultDto> ChangeUserRole(Guid userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new ResultDto(new List<string> { "Пользователь не найден" });
            }

            // Удаление всех текущих ролей
            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                return new ResultDto(removeResult.Errors.Select(e => e.Description).ToList());
            }

            // Добавление новой роли
            var addResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!addResult.Succeeded)
            {
                return new ResultDto(addResult.Errors.Select(e => e.Description).ToList());
            }

            return new ResultDto();
        }

        /// <summary>
        /// Выключение/Выключение уведомлений
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        public async Task<ResultDto> NotificationSwitch(long telegramId)
        {
            await _userRepository.NotificationSwitch(telegramId);

            return new ResultDto();
        }

        /// <summary>
        /// Изменить частоту уведомлений
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public async Task<ResultDto> ChangeNotificationFrequency(long telegramId, int frequency)
        {
            await _userRepository.ChangeNotificationFrequency(telegramId, frequency);

            return new ResultDto();
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResultDto> UpdateUser(UserUpdateDto model)
        {
            await _userRepository.UpdateUser(model);

            return new ResultDto();
        }

        /// <summary>
        /// Переключение активного курса
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        public async Task<ResultDto> SwitchActiveCourse(long telegramId)
        {
            await _userRepository.SwitchActiveCourse(telegramId);

            return new ResultDto();
        }
    }
}
