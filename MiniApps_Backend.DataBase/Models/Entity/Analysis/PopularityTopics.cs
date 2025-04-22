using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.Analysis
{
    public class VisitLesson
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid LessonId { get; set; }

        [JsonIgnore]
        public string LessonTitle { get; set; } = string.Empty;
    }
}
