namespace MiniApps_Backend.DataBase.Models.Entity.ManyToMany
{
    /// <summary>
    /// Дто для подписки
    /// </summary>
    public class CourseSubscriptionDto
    {
        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public long TelegramId { get; set; }
    }
}
