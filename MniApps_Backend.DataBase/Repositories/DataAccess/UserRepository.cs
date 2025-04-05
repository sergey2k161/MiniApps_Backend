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

        public async Task AddUser(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                var user = await _context.Users.FindAsync(telegramId);

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
