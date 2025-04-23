using Microsoft.AspNetCore.Identity;
using MiniApps_Backend.DataBase;
using MiniApps_Backend.DataBase.Models.Entity;

namespace MiniApps_Backend.Abstractions
{
    public static class IdentityExtension
    {
        public static void AddAppIdentity(this IServiceCollection services)
        {
            services.AddIdentity<CommonUser, IdentityRole<Guid>>()
                .AddRoles<IdentityRole<Guid>>() 
                .AddEntityFrameworkStores<MaDbContext>()
                .AddDefaultTokenProviders();
        }
    }

}
