using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Repositories.Interfaces;

namespace MiniApps_Backend.DataBase.Repositories.DataAccess
{
    public class WalletRepository : IWalletRepository
    {
        private readonly MaDbContext _context;

        public WalletRepository(MaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> CreateTransaction(Transaction transactions)
        {
            try
            {
                await _context.AddAsync(transactions);
                await _context.SaveChangesAsync();

                return new ResultDto();
            }
            catch (Exception ex)
            {
                return new ResultDto(new List<string> { $"Произошла ошибка в БД: {ex}" });
            }
        }

        public async Task<ResultDto> CreateWallet(Wallet wallet)
        {
            try
            {
                await _context.AddAsync(wallet);
                await _context.SaveChangesAsync();

                return new ResultDto();
            }
            catch (Exception ex)
            {
                return new ResultDto(new List<string> { $"Произошла ошибка в БД: {ex}" });
            }
        }

        public async Task<decimal> GetBalance(long telegramId)
        {
            var wallet = await _context.Users
                .Where(u => u.TelegramId == telegramId)
                .Select(u => u.Wallet.Balance)
                .FirstOrDefaultAsync();

            return wallet;
        }

        public async Task<Wallet> GetWallet(Wallet wallet)
        {
            return await _context.Wallets.FirstOrDefaultAsync(w => w.Id == wallet.Id);
        }
    }
}
