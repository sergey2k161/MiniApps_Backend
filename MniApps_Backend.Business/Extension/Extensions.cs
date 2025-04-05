using Microsoft.Extensions.DependencyInjection;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.Business.Services.Logic;

namespace MiniApps_Backend.Business.Extension
{
    public static class Extensions
    {
        public static IServiceCollection AddBussiness(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
