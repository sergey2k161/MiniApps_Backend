using MiniApps_Backend.DataBase.Extension;
using MiniApps_Backend.Business.Extension;
using MiniApps_Backend.Bot.Extention;
using Microsoft.AspNetCore.Identity;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase;

namespace MiniApps_Backend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddDataBase(configuration);
            builder.Services.AddBussiness(configuration);
            builder.Services.AddTelegramBot(configuration);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSwaggerGen();

            builder.Services.AddIdentity<CommonUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<MaDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("https://kmp3b968-3000.euw.devtunnels.ms")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    await RoleInitializer.SeedRoles(services);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while seeding roles: {ex.Message}");
                }
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseCors("AllowFrontend");

            //using (var scope = app.Services.CreateScope())
            //{
            //    var scopeFactory = scope.ServiceProvider.GetRequiredService<IServiceScopeFactory>();
            //    BotChat.InitializeTelegramBot(configuration, scopeFactory);
            //}

            app.Run();
        }

        
    }
}
