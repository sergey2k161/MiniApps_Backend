using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;

namespace MiniApps_Backend.DataBase.Models.Dto.CourseConstructor
{
    public class CourseDto
    {
        public string Title { get; set; }

        public string? UrlVideo { get; set; }

        public string BriefDescription { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool Discount { get; set; }

        public decimal PriceWithDiscount { get; set; }

        public List<LessonDto> Lessons { get; set; }
    }
}
