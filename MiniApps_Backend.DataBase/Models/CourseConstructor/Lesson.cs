using System.ComponentModel.DataAnnotations.Schema;

namespace MiniApps_Backend.DataBase.Models.CourseConstructor
{
    public class Lesson
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string BriefDescription { get; set; }

        public string Description { get; set; }

        public string? UrlVideo { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public int Experience { get; set; }

        public Guid TestId { get; set; }
        public Test Test {  get; set; }
    }
}
