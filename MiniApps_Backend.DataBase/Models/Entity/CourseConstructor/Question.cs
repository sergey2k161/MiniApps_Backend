using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.CourseConstructor
{
    public class Question
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Explanation { get; set; }

        public Guid TestId { get; set; }

        [JsonIgnore]
        public Test Test { get; set; }

        public List<Answer> Answers { get; set; } 
    }
}

