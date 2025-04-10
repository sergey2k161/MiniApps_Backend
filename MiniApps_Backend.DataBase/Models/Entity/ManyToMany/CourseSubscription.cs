using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.ManyToMany
{
    public class CourseSubscription
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
