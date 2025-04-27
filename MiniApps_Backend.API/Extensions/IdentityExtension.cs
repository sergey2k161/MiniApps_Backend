using Microsoft.AspNetCore.Identity;
using MiniApps_Backend.DataBase;
using MiniApps_Backend.DataBase.Models.Entity;

namespace MiniApps_Backend.Abstractions
{
    /// <summary>
    /// Расширения для работы с идентификацией
    /// </summary>
    public static class IdentityExtension
    {
        /// <summary>
        /// Метод расширения для добавления идентификации
        /// </summary>
        /// <param name="services"></param>
        public static void AddAppIdentity(this IServiceCollection services)
        {
            services.AddIdentity<CommonUser, IdentityRole<Guid>>()
                .AddRoles<IdentityRole<Guid>>() 
                .AddEntityFrameworkStores<MaDbContext>()
                .AddDefaultTokenProviders();
        }
    }

}
