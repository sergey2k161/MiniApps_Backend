namespace MiniApps_Backend.Abstractions
{
    public static class CorsExtension
    {
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
