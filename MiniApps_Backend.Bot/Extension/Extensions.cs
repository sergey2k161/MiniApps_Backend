using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniApps_Backend.Bot.File;
using MiniApps_Backend.Bot.Handlers;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.Business.Services.Logic;
using Telegram.Bot;

namespace MiniApps_Backend.Bot.Extention
{
    public static class Extensions
    {
        /// <summary>
        /// Добавление TelegramBotClient и других сервисов бота
        /// </summary>
        public static IServiceCollection AddTelegramBot(this IServiceCollection services, IConfiguration config)
        {
            var token = config["TelegramBotToken"]
                ?? throw new ArgumentNullException("TelegramBotToken");

            services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(token));

            // Зарегистрируй BotInitializer как Scoped
            services.AddScoped<BotInitializer>();
            services.AddScoped<IBotService, BotService>();
            services.AddHostedService<BotInitializer>();

            return services;
        }
    }
}
