using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.CourseConstructor
{
    public class Answer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public bool IsCorrect { get; set; }

        public string Explanation { get; set; }

        public Guid QuestionId { get; set; }

        [JsonIgnore]
        public Question Question { get; set; }
    }
}
