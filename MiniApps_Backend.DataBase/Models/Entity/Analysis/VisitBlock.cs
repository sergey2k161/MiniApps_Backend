using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.Analysis
{
    public class VisitBlock
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid BlockId { get; set; }

        [JsonIgnore]
        public string BlockTitle { get; set; } = string.Empty;

        public long TelegramId { get; set; }

        [JsonIgnore]
        public DateTime VisitAt { get; set; }
    }
}
