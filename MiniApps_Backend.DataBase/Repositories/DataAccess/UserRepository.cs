using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
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

                int userLevel = user.Level.Value;
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

    }
}
