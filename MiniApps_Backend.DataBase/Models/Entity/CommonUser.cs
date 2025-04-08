using Microsoft.AspNetCore.Identity;

namespace MiniApps_Backend.DataBase.Models.Entity
{
    public class CommonUser : IdentityUser<Guid>
    {
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
