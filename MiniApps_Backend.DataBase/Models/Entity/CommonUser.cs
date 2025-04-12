using Microsoft.AspNetCore.Identity;

namespace MiniApps_Backend.DataBase.Models.Entity
{
    /// <summary>
    /// Сущность айдентити
    /// </summary>
    public class CommonUser : IdentityUser<Guid>
    {
        /// <summary>
        /// Ссылка на пользователя
        /// </summary>
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
