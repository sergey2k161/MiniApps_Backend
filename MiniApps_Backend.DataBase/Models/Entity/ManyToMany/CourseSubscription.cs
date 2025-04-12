using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.ManyToMany
{
    /// <summary>
    /// Сущность подпищиков курса
    /// </summary>
    public class CourseSubscription
    {
        /// <summary>
        /// Ссылка на курс
        /// </summary>
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        /// <summary>
        /// Ссылка на пользователя
        /// </summary>
        public long TelegramId { get; set; }
        public User User { get; set; }

    }
}
