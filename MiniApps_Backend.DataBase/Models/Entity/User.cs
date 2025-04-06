using System.ComponentModel.DataAnnotations.Schema;

namespace MiniApps_Backend.DataBase.Models.Entity
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public long TelegramId { get; set; }  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public int? Experience { get; set; } = 0;
    }
}
