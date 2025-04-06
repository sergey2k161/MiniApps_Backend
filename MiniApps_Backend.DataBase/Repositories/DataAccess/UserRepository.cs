using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Repositories.Interfaces;

namespace MiniApps_Backend.DataBase.Repositories.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly MaDbContext _context;

        public UserRepository(MaDbContext context)
        {
            _context = context;
        }

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
    }
}
