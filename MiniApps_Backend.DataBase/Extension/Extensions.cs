using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniApps_Backend.DataBase.Repositories.DataAccess;
using MiniApps_Backend.DataBase.Repositories.Interfaces;

namespace MiniApps_Backend.DataBase.Extension
{
    /// <summary>
    /// Расширение слоя БД
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Добавление слоя БД
        /// </summary>
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            // Репозитории
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();

            // Подключение БД
            services.AddDbContext<MaDbContext>(x =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Строка подключения отсутствует!");
                }
                
                x.UseNpgsql(connectionString);
            });

            return services;
        }
    }
}
