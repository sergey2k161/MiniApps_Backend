namespace MiniApps_Backend.DataBase.Models.Entity.ManyToMany
{
    public class CourseSubscriptionDto
    {
        public Guid CourseId { get; set; }

        public Guid UserId { get; set; }
    }
}
