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

                if (existUser == null)
                {
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();

                    return new ResultDto();
                }

                return new ResultDto(new List<string> { "Ошибка БД: Email уже занят" });
            }
            catch (Exception ex)
            {
                return new ResultDto(new List<string> { $"Произошла ошибка в БД: {ex}" });
            }

        }

        /// <summary>
        /// Получение пользователя по Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                ArgumentNullException.ThrowIfNull(user);

                return user;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (Exception ex)  
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Получение пользователя по Telegram Id
        /// </summary>
        /// <param name="telegramId">Telegram Id</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<User> GetUserByTelegramId(long telegramId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == telegramId);

                ArgumentNullException.ThrowIfNull(user);

                return user;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

                int userLevel = user.Level ?? 0;
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
            catch (Exception ex)
            {
                return new ResultDto(new List<string> { $"Ошибка при обновлении уровня: {ex.Message}" });
            }
        }
    }
}
