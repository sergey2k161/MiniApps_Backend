using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.Ammount;

namespace MiniApps_Backend.Business.Services.Interfaces
{
    public interface IWalletService
    {
        Task<Wallet> CreateWallet(User user);

        Task<ResultDto> CreateTransaction
            (long telegramId, bool type, bool withDiscount, double percentageDiscounts, decimal total);

        Task<ResultDto> UpdateBalance(Guid walletId, bool type, decimal total);

        Task<decimal> GetBalance(long telegramId);
    }
}
