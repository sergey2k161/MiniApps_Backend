namespace MiniApps_Backend.API.Extensions
{
    /// <summary>
    /// Расширения для кэша
    /// </summary>
    public static class CacheExtension
    {
        /// <summary>
        /// Метод расширения для добавления Redis кэша
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            // Добавление Redis кэша
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "MiniApps_";
            });
        }
    }

}
