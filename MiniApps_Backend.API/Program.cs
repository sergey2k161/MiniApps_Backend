using MiniApps_Backend.DataBase.Extension;
using MiniApps_Backend.Business.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Threading;
using MiniApps_Backend.API;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.Bot.Extention;

namespace MiniApps_Backend
{
    public class Program
    {
        public static void Main(string[] args)
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
