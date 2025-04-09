using System.ComponentModel.DataAnnotations.Schema;

namespace MiniApps_Backend.DataBase.Models.Entity
{
    public class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public Guid UserId { get; set; }
        public User User { get; set; }

        public DateTime CreateAt { get; set; }
        
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
        public double? PercentageDiscounts { get; set; }
    }
}
