using System.ComponentModel.DataAnnotations.Schema;

namespace MiniApps_Backend.DataBase.Models.Entity.Ammount
{
    /// <summary>
    /// Сущность кошелек
    /// </summary>
    public class Wallet
    {
        /// <summary>
        /// Идентификатор кошелька
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Владелец кошелька
        /// </summary>
        public Guid UserId { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Баланс
        /// </summary>
        public decimal Balance { get; set; } = 0;
    }
}
