using MiniApps_Backend.DataBase.Models.Entity;

namespace MiniApps_Backend.DataBase.Models.Dto
{
    public class TransactionDto
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public long TelegramId { get; set; }

        /// <summary>
        /// Тип транзакции пополнение/списание
        /// </summary>
        public bool Type { get; set; }

        /// <summary>
        /// Наличин скидки у транзакции
        /// </summary>
        public bool WithDiscount { get; set; }

        /// <summary>
        /// Процент скидки
        /// </summary>
        public double PercentageDiscounts { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Total { get; set; }
    }
}
