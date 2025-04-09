using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.CourseConstructor
{
    public class Test
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid LessonId { get; set; }

        [JsonIgnore]
        public Lesson Lesson { get; set; }

        public List<Question> Questions {  get; set; }
    }
}
