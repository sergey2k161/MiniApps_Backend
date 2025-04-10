using MiniApps_Backend.DataBase.Models.Entity.ManyToMany;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniApps_Backend.DataBase.Models.Entity.CourseConstructor
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string? UrlVideo { get; set; }

        public string BriefDescription { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool Discount { get; set; }

        public decimal? PriceWithDiscount { get; set; }

        public DateTime CreateAt { get; set; }

        public List<Lesson> Lessons { get; set; } //

        public int Experience { get; set; }

        //public List<CourseSubscription> CourseSubscriptions { get; set; }
        public List<User> Users { get; set; }
    }
}

