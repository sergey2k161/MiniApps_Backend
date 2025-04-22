using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Dto.CourseConstructor
{
    public class BlockSucsessDto
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public long TelegramId { get; set; }

        public Guid BlockId { get; set; }

        public DateTime? FinishAt { get; set; } = null;

        public bool Finish { get; set; }
    }
}
