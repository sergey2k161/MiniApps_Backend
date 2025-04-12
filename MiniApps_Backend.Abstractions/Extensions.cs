using MiniApps_Backend.Bot.File;
using MiniApps_Backend.Bot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace MiniApps_Backend.Abstractions
{
    public static class Extensions
    {
        public static IServiceCollection AddAbstractions(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IBotService, BotService>();

            return services;
        }
    }
}
