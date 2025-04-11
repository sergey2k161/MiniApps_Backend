namespace MiniApps_Backend.DataBase.Models.Entity.ManyToMany
{
    public class CourseSubscriptionDto
    {
        public Guid CourseId { get; set; }

        public long TelegramId { get; set; }
    }
}
