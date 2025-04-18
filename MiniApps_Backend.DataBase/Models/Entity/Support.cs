using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity
{
    public class Support
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public long UserTelegramId { get; set; }

        public bool InWork { get; set; }

        public string Status { get; set; } = string.Empty;

        public long? Helper { get; set; }

        public string Process { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}
