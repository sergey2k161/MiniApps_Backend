using System.ComponentModel.DataAnnotations.Schema;

namespace MiniApps_Backend.DataBase.Models.CourseConstructor
{
    public class Answer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public string Explanation { get; set; }

        public Guid QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
