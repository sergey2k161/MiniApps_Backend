using MiniApps_Backend.DataBase.Extension;
using MiniApps_Backend.Business.Extension;
using MiniApps_Backend.Bot.Extention;
using MiniApps_Backend.Abstractions;
using MiniApps_Backend.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MiniApps_Backend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // 1. Конфигурация авторизации и внешних зависимостей
            //AuthHelper.ConfigureServices(builder.Services, builder);

            // 2. Swagger
            SwaggerHelper.AddSwagger(builder.Services);

            // 3. Контроллеры и JSON
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            builder.Services.AddOpenApi();

            // 4. Базовые сервисы и DI
            builder.Services.AddScoped<TokenManager>();
            builder.Services.AddHttpContextAccessor();

            // 5. Подключение бизнес-логики
            builder.Services.AddDataBase(configuration);
            builder.Services.AddBussiness(configuration);
            builder.Services.AddTelegramBot(configuration);
            builder.Services.AddAbstractions(configuration);

            // 6. Identity, CORS, Cache
            builder.Services.AddAppIdentity();
            builder.Services.AddCustomCors(configuration);
            builder.Services.AddRedisCache(configuration);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // Можно также вытаскивать токен из cookie (если используется)
                        if (context.Request.Cookies.ContainsKey("token"))
                        {
                            context.Token = context.Request.Cookies["token"];
                        }

                        return Task.CompletedTask;
                    }
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                    )
                };
            });
            builder.Services.AddAuthorization();

            // 7. Построение приложения
            var app = builder.Build();

            // 8. Swagger
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // 9. Сидинг ролей
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var db = services.GetRequiredService<MiniApps_Backend.DataBase.MaDbContext>();
                db.Database.Migrate();

                await RoleSeeder.SeedAsync(services);
            }


            // 10. Middleware пайплайн
            app.UseHttpsRedirection();
            app.UseCors("AllowFrontend");
            app.UseAuthentication();
            app.UseAuthorization();

            // 11. Контроллеры
            app.MapControllers();

            // 12. Запуск приложения
            app.Run();
        }
    }
}
