using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniApps_Backend.Business.Mapping;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.Business.Services.Logic;

namespace MiniApps_Backend.Business.Extension
{
    /// <summary>
    /// Расширения для бизнес логики
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Добавление бизнес логики
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBussiness(this IServiceCollection services, IConfiguration configuration)
        {
            // Сервисы
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<NotificationService>();
            services.AddHostedService<NotificationWorker>();


            // Маппинг
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
