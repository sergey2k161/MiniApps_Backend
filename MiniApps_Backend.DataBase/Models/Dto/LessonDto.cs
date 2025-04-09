using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniApps_Backend.DataBase.Models.Dto
{
    public class LessonDto
    {
        public string Title { get; set; }

        public string BriefDescription { get; set; }

        public string Description { get; set; }

        public string? UrlVideo { get; set; }

        public int Experience { get; set; }

        public TestDto Test { get; set; } 
    }
}
