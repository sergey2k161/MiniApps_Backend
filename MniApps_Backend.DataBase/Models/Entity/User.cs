using System.ComponentModel.DataAnnotations.Schema;

namespace MiniApps_Backend.DataBase.Models.Entity
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public long TelegramId { get; set; }
        
        public int Experience { get; set; } 
    }
}
