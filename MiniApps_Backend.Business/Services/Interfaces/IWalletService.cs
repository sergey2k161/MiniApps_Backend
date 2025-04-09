using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;

namespace MiniApps_Backend.Business.Services.Interfaces
{
    public interface IWalletService
    {
        Task<Wallet> CreateWallet(User user);
    }
}
