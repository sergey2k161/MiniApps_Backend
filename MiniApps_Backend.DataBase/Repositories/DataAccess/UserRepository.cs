using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.Ammount;
using MiniApps_Backend.DataBase.Repositories.Interfaces;

namespace MiniApps_Backend.DataBase.Repositories.DataAccess
{
    /// <summary>
    /// Репозиторий пользователя
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly MaDbContext _context;

        /// <summary>
        /// Конструктор UserRepository
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(MaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавление нового пользователя в БД
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        /// <returns></returns>
        public async Task<ResultDto> AddUser(User user)
        {
            try
            {
                var existUser = await _context.Users.FirstOrDefaultAsync(u => u.TelegramId == user.TelegramId);

                var checkEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

                if (existUser != null || checkEmail != null)
                {
                    return new ResultDto(new List<string> { "Ошибка БД: Email и TelegramId должны быть уникальными" });
                }

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Произошла ошибка в БД" });
            }

        }

        /// <summary>
        /// Изменение частоты уведомлений
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public async Task<ResultDto> ChangeNotificationFrequency(long telegramId, int frequency)
        {
            await _context.Users
                .Where(u => u.TelegramId == telegramId)
                .ExecuteUpdateAsync(u => u.SetProperty(u => u.NotificationFrequency, frequency));

            return new ResultDto();
        }

        /// <summary>
        /// Список всех пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Список идентификаторов курсов, на которые подписан пользователь
        /// </summary>
        /// <param name="telegramId">Идентификтор пользователя</param>
        /// <returns>Список Идентификторов</returns>
        public async Task<List<Guid>> GetSubscribesList(long telegramId)
        {
            var subsсribeList = await _context.CourseSubscriptions
                .Where(t => t.TelegramId == telegramId)
                .Select(t => t.CourseId)
                .ToListAsync();

            return subsсribeList;
        }

        /// <summary>
        /// Получение пользователя по Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<User> GetUserById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        /// <summary>
        /// Получение пользователя по Telegram Id
        /// </summary>
        /// <param name="telegramId">Telegram Id</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<User> GetUserByTelegramId(long telegramId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == telegramId);
        }

        /// <summary>
        /// Получить список пользователей, которые подписаны на уведомления
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsersForNotification()
        {
            var users = await _context.Users
                .Where(u => u.TurnNotification == true && u.ActiveCourse == true)
                .ToListAsync();

            if (users == null)
            {
                return new List<User>();
            }

            return users;
        }

        /// <summary>
        /// Изменение состояния уведомлений
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        public async Task<ResultDto> NotificationSwitch(long telegramId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);

                if (user == null)
                {
                    return new ResultDto(new List<string> { $"Пользователь не найден" });
                }

                await _context.Users
                    .Where(u => u.TelegramId == telegramId)
                    .ExecuteUpdateAsync(u => u.SetProperty(u => u.TurnNotification, !user.TurnNotification));

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }

        }

        /// <summary>
        /// Изменение состояния активного курса
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        public async Task<ResultDto> SwitchActiveCourse(long telegramId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);

                if (user == null)
                {
                    return new ResultDto(new List<string> { $"Пользователь не найден" });
                }

                await _context.Users
                    .Where(u => u.TelegramId == telegramId)
                    .ExecuteUpdateAsync(u => u.SetProperty(u => u.ActiveCourse, !user.ActiveCourse));

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }

        }

        /// <summary>
        /// Обновление уровня пользователя, в зависимости от опыта
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        public async Task<ResultDto> UpdateLevelUser(User user)
        {
            try
            {
                const int x = 500;
                const int y = 2;

                int userLevel = user.Level ?? 1;
                var userExperience = user.Experience;

                int nextLevel = userLevel + 1;

                // формула из игры Dungeons & Dragons
                double requiredExp = x * Math.Pow(nextLevel, y) - (x * nextLevel);

                while (userExperience >= requiredExp)
                {
                    userLevel++;
                    nextLevel = userLevel + 1;
                    requiredExp = x * Math.Pow(nextLevel, y) - (x * nextLevel);
                }

                await _context.Users
                    .Where(u => u.Id == user.Id)
                    .ExecuteUpdateAsync(u => u.SetProperty(u => u.Level, userLevel));

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResultDto> UpdateUser(UserUpdateDto model)
        {
            await _context.Users
                    .Where(u => u.TelegramId == model.TelegramId)
                    .ExecuteUpdateAsync(u => u
                        .SetProperty(u => u.Email, model.Email)
                        .SetProperty(u => u.RealFirstName, model.RealFirstName)
                        .SetProperty(u => u.RealLastName, model.RealLastName)
                        .SetProperty(u => u.NotificationFrequency, model.NotificationFrequency)
                        .SetProperty(u => u.TurnNotification, model.TurnNotification));

            return new ResultDto();

        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="commonUserId">Идентификтор с таблицы CommonUser</param>
        /// <param name="walletId">Идентификтор кошелька</param>
        /// <returns>Результат обновления данных</returns>
        public async Task<ResultDto> UpdateUserAsync(User user, Guid commonUserId, Guid walletId)
        {
            try
            {
                await _context.Users
                    .Where(u => u.Id == user.Id)
                    .ExecuteUpdateAsync(u => u.SetProperty(u => u.CommonUserId, commonUserId));
                
                await _context.Users
                    .Where(u => u.Id == user.Id)
                    .ExecuteUpdateAsync(u => u.SetProperty(u => u.WalletId, walletId));

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }
        }

        public async Task<ResultDto> UpdateExpForUser(long telegramId, int exp)
        {
            try
            {
                await _context.Users
                .Where(u => u.TelegramId == telegramId)
                    .ExecuteUpdateAsync(u => u.SetProperty(u => u.Experience, exp));

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }
        }
    }
}
