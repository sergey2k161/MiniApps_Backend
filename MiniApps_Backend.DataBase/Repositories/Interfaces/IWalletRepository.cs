using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;

namespace MiniApps_Backend.DataBase.Repositories.Interfaces
{
    public interface IWalletRepository
    {
        Task<ResultDto> CreateWallet(Wallet wallet);

        Task<decimal> GetBalance(long telegramId);

        Task<ResultDto> CreateTransaction(Transaction transaction);

        Task<Wallet> GetWallet(Wallet wallet);
    }
}
