using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity.Ammount;
using MiniApps_Backend.DataBase.Repositories.Interfaces;

namespace MiniApps_Backend.DataBase.Repositories.DataAccess
{
    /// <summary>
    /// Репозиторий кошелька
    /// </summary>
    public class WalletRepository : IWalletRepository
    {
        private readonly MaDbContext _context;

        /// <summary>
        /// Конструктор репозитория кошелька
        /// </summary>
        /// <param name="context"></param>
        public WalletRepository(MaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Создание кошелька
        /// </summary>
        /// <param name="wallet">Сущность кошелька</param>
        /// <returns>Результат создания</returns>
        public async Task<ResultDto> CreateWallet(Wallet wallet)
        {
            try
            {
                await _context.AddAsync(wallet);
                await _context.SaveChangesAsync();

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Произошла ошибка в БД" });
            }
        }

        /// <summary>
        /// Получение баланка пользователя
        /// </summary>
        /// <param name="telegramId">Идентификтор пользователя</param>
        /// <returns></returns>
        public async Task<decimal> GetBalance(long telegramId)
        {
            var wallet = await _context.Users
                .Where(u => u.TelegramId == telegramId)
                .Select(u => u.Wallet.Balance)
                .FirstOrDefaultAsync();

            return wallet;
        }

        /// <summary>
        /// Изменение баланка сользователя
        /// </summary>
        /// <param name="wallet">Сущность кошелек</param>
        /// <param name="total">Сумма</param>
        /// <returns>Результат изменения</returns>
        public async Task<ResultDto> UpdateBalace(Wallet wallet, decimal total)
        {
            try
            {
                await _context.Wallets
                    .Where(w => w.Id == wallet.Id)
                    .ExecuteUpdateAsync(w => w
                        .SetProperty(w => w.Balance, total));

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Произошла ошибка в БД" });
            }
        }

        /// <summary>
        /// Создание транзакции
        /// </summary>
        /// <param name="transaction">Сущность транзакции</param>
        /// <returns>Результат создания транзакции</returns>
        public async Task<ResultDto> CreateTransaction(Transaction transactions)
        {
            try
            {
                await _context.AddAsync(transactions);
                await _context.SaveChangesAsync();

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Произошла ошибка в БД" });
            }
        }

        /// <summary>
        /// Получения кошелька пользователя
        /// </summary>
        /// <param name="telegramId">Идентификтор пользователя</param>
        /// <returns>Кошелек</returns>
        public async Task<Wallet> GetWalletByTelegramId(long telegramId)
        {
            return await _context.Users
                .Where(u => u.TelegramId == telegramId)
                .Select(w => w.Wallet)
                .FirstOrDefaultAsync();
        }

    }
}
