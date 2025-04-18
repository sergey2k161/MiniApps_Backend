using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.Ammount;
using MiniApps_Backend.DataBase.Repositories.Interfaces;

namespace MiniApps_Backend.Business.Services.Logic
{
    /// <summary>
    /// Сервис кошелька
    /// </summary>
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Конструктор сервиса кошелька
        /// </summary>
        /// <param name="context"></param>
        public WalletService(IWalletRepository walletRepository, IUserRepository userRepository)
        {
            _walletRepository = walletRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Создание транзацкции
        /// </summary>
        /// <param name="telegramId">Ид пользователя</param>
        /// <param name="type">Тип</param>
        /// <param name="withDiscount">Скидака есть или нет</param>
        /// <param name="percentageDiscounts">Процент скидки</param>
        /// <param name="total">Сумма</param>
        /// <returns>Результат создания</returns>
        public async Task<ResultDto> CreateTransaction
            (long telegramId, bool type, bool withDiscount, double percentageDiscounts, decimal total)
        {
            try
            {
                var balance = await _walletRepository.GetBalance(telegramId);
                var user = await _userRepository.GetUserByTelegramId(telegramId);
                var wallet = await _walletRepository.GetWalletByTelegramId(telegramId);

                if (user == null)
                {
                    ArgumentNullException.ThrowIfNull(user);
                }

                if (!type)
                {
                    var totalWithDiscount = total - ((decimal)percentageDiscounts / 100 * total);

                    var newBalance = balance - totalWithDiscount;

                    if (newBalance < 0)
                    {
                        return new ResultDto(new List<string> { $"Ошибка, недостаточно средств" });
                    }

                    if (withDiscount)
                    {
                        var transaction = new Transaction
                        {
                            UserId = user.Id,
                            CreateAt = DateTime.UtcNow,
                            Type = type,
                            WithDiscount = withDiscount,
                            PercentageDiscounts = percentageDiscounts,
                            Total = totalWithDiscount
                        };

                        await _walletRepository.CreateTransaction(transaction);
                    }
                    else
                    {
                        var transaction = new Transaction
                        {
                            UserId = user.Id,
                            CreateAt = DateTime.UtcNow,
                            Type = type,
                            WithDiscount = withDiscount,
                            PercentageDiscounts = 0.0,
                            Total = total
                        };

                        await _walletRepository.CreateTransaction(transaction);
                    }
                    

                    await _walletRepository.UpdateBalace(wallet, newBalance);
                }
                else
                {
                    var transaction = new Transaction
                    {
                        UserId = user.Id,
                        CreateAt = DateTime.UtcNow,
                        Type = type,
                        WithDiscount = null,
                        PercentageDiscounts = null,
                        Total = total
                    };

                    await _walletRepository.CreateTransaction(transaction);

                    var newBalance = balance + total;

                    await _walletRepository.UpdateBalace(wallet, newBalance);
                }

                return new ResultDto();
            }
            catch (Exception ex)
            {
                return new ResultDto(new List<string> { $"Обнаружена ошибка {ex}" });
            }
        }

        /// <summary>
        /// Создание кошелька
        /// </summary>
        /// <param name="user">Владелец</param>
        /// <returns>Кошелек</returns>
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

        /// <summary>
        /// Получить баланс
        /// </summary>
        /// <param name="telegramId">ИД пользователя</param>
        /// <returns>сумма</returns>
        public async Task<decimal> GetBalance(long telegramId)
        {
            return await _walletRepository.GetBalance(telegramId);
        }

        /// <summary>
        /// Изменить баланс
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public async Task<ResultDto> UpdateBalanse(long telegramId, decimal total)
        {
            await _walletRepository.UpdateBalanse(telegramId, total);

            return new ResultDto();
        }

        ///// <summary>
        ///// Обновить баланс кошелька
        ///// </summary>
        ///// <param name="walletId"></param>
        ///// <param name="type"></param>
        ///// <param name="total"></param>
        ///// <returns></returns>
        ///// <exception cref="NotImplementedException"></exception>
        //public Task<ResultDto> UpdateBalance(Guid walletId, bool type, decimal total)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
