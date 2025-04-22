using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Dto.CourseConstructor
{
    public class CourseSucsessDto
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public long TelegramId { get; set; }

        public Guid CourseId { get; set; }

        public bool Finish {get; set;}
    }
}
