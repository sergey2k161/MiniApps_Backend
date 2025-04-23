namespace MiniApps_Backend.API.Extensions
{
    public static class CacheExtension
    {
        public static void AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "MiniApps_";
            });
        }
    }

}
