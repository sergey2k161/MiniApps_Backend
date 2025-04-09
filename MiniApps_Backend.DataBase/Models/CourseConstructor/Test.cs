using System.ComponentModel.DataAnnotations.Schema;

namespace MiniApps_Backend.DataBase.Models.CourseConstructor
{
    public class Test
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public List<Question> Questions {  get; set; }
    }
}
