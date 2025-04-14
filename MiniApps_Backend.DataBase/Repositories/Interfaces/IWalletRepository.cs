using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.Ammount;

namespace MiniApps_Backend.DataBase.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория кошелька
    /// </summary>
    public interface IWalletRepository
    {
        /// <summary>
        /// Создание кошелька
        /// </summary>
        /// <param name="wallet">Сущность кошелька</param>
        /// <returns>Результат создания</returns>
        Task<ResultDto> CreateWallet(Wallet wallet);

        /// <summary>
        /// Получение баланка пользователя
        /// </summary>
        /// <param name="telegramId">Идентификтор пользователя</param>
        /// <returns></returns>
        Task<decimal> 
            GetBalance(long telegramId);

        /// <summary>
        /// Изменение баланка сользователя
        /// </summary>
        /// <param name="wallet">Сущность кошелек</param>
        /// <param name="total">Сумма</param>
        /// <returns>Результат изменения</returns>
        Task<ResultDto> UpdateBalace(Wallet wallet, decimal total);

        /// <summary>
        /// Создание транзакции
        /// </summary>
        /// <param name="transaction">Сущность транзакции</param>
        /// <returns>Результат создания транзакции</returns>
        Task<ResultDto> CreateTransaction(Transaction transaction);

        /// <summary>
        /// Получения кошелька пользователя
        /// </summary>
        /// <param name="telegramId">Идентификтор пользователя</param>
        /// <returns>Кошелек</returns>
        Task<Wallet> GetWalletByTelegramId(long telegramId);

        /// <summary>
        /// Изменить баланс пользователя
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<ResultDto> UpdateBalanse(long telegramId, decimal total);

    }
}
