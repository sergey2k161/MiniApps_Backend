using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.Ammount;

namespace MiniApps_Backend.Business.Services.Interfaces
{
    /// <summary>
    /// Интерфейс для сервиса кошелька
    /// </summary>
    public interface IWalletService
    {
        /// <summary>
        /// Создание кошелька
        /// </summary>
        /// <param name="user">Владелец</param>
        /// <returns>Кошелек</returns>
        Task<Wallet> CreateWallet(User user);

        /// <summary>
        /// Создание транзацкции
        /// </summary>
        /// <param name="telegramId">Ид пользователя</param>
        /// <param name="type">Тип</param>
        /// <param name="withDiscount">Скидака есть или нет</param>
        /// <param name="percentageDiscounts">Процент скидки</param>
        /// <param name="total">Сумма</param>
        /// <returns>Результат создания</returns>
        Task<ResultDto> CreateTransaction
            (long telegramId, bool type, bool withDiscount, double percentageDiscounts, decimal total);

        ///// <summary>
        ///// Изменение баланка сользователя
        ///// </summary>
        ///// <param name="wallet">Сущность кошелек</param>
        ///// <param name="total">Сумма</param>
        ///// <returns>Результат изменения</returns>
        //Task<ResultDto> UpdateBalance(Guid walletId, bool type, decimal total);

        /// <summary>
        /// Получить баланс
        /// </summary>
        /// <param name="telegramId">ИД пользователя</param>
        /// <returns>сумма</returns>
        Task<decimal> GetBalance(long telegramId);

        /// <summary>
        /// Изменить баланс
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<ResultDto> UpdateBalanse(long telegramId, decimal total);
    }
}
