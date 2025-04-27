namespace MiniApps_Backend.Abstractions
{
    /// <summary>
    /// Расширения для CORS
    /// </summary>
    public static class CorsExtension
    {
        /// <summary>
        /// Метод расширения для добавления CORS
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("https://kmp3b968-3000.euw.devtunnels.ms")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
        }
    }

}
