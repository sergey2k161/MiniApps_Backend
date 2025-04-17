using MiniApps_Backend.DataBase.Extension;
using MiniApps_Backend.Business.Extension;
using MiniApps_Backend.Bot.Extention;
using Microsoft.AspNetCore.Identity;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase;
using MiniApps_Backend.Abstractions;
using MiniApps_Backend.API;

namespace MiniApps_Backend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            Helper.ConfigureServices(builder.Services, builder);
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

                });



            builder.Services.AddOpenApi();

            builder.Services.AddDataBase(configuration);
            builder.Services.AddBussiness(configuration);
            builder.Services.AddTelegramBot(configuration);
            builder.Services.AddAbstractions(configuration);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "MiniApp API",
                    Version = "v1"
                });

                // Добавляем схему авторизации Bearer
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "Введите JWT токен в формате: Bearer {your token}",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Применяем эту схему ко всем контроллерам
                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });


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

            //builder.WebHost.ConfigureKestrel(options =>
            //{
            //    var certPath = builder.Configuration["Kestrel:Certificates:Default:Path"];
            //    var certPassword = builder.Configuration["Kestrel:Certificates:Default:Password"];

            //    options.Listen(IPAddress.Any, 7137, listenOptions =>
            //    {
            //        listenOptions.UseHttps(certPath, certPassword);
            //    });
            //});

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("Redis");
                options.InstanceName = "MiniApps_";
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
