using System.ComponentModel.DataAnnotations.Schema;

namespace MiniApps_Backend.DataBase.Models.Entity
{
    public class Wallet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        //public long TelegramId { get; set; }
        public User User { get; set; }

        public decimal Balance { get; set; } = 0;
    }
}
