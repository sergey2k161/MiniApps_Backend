using System.ComponentModel.DataAnnotations.Schema;

namespace MiniApps_Backend.DataBase.Models.CourseConstructor
{
    public class Question
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Explanation { get; set; }

        public Guid TestId { get; set; }
        public Test Test { get; set; }

        public List<Answer> Answers { get; set; } 
    }
}
