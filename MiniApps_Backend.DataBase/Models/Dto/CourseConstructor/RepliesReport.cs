using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Dto.CourseConstructor
{
    public class RepliesReport
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public long TelegramId { get; set; }

        public Guid QuestionId { get; set; }

        public bool Answer {  get; set; }

        public Guid TestId {  get; set; }
    }
}
