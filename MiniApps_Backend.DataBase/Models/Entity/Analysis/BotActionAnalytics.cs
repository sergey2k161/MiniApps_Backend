using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.Analysis
{
    public class BotActionAnalytics
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ActionName { get; set; } = string.Empty;

        public DateTime ExecutedAt { get; set; } 

        public string Result { get; set; } = string.Empty;

        public long? UserId { get; set; } 

    }
}
