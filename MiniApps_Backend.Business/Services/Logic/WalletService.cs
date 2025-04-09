using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Repositories.Interfaces;

namespace MiniApps_Backend.Business.Services.Logic
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<Wallet> CreateWallet(User user)
        {
            var newWallet = new Wallet
            {
                UserId = user.Id,
                Balance = 0
            };

            await _walletRepository.CreateWallet(newWallet);

            return newWallet;
        }
    }
}
