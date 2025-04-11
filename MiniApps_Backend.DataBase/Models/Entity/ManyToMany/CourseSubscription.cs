using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.ManyToMany
{
    public class CourseSubscription
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public long TelegramId { get; set; }
        public User User { get; set; }

    }
}
