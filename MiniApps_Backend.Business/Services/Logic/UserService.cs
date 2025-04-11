using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;

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

                // Выдача роли по умолчанию
                var roleResult = await _userManager.AddToRoleAsync(commonUser, "User");

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
                    RealLastName = userRequest.RealLastName
                };

                await _userRepository.AddUser(user);

                return user;
            }

            return null;
        }

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
