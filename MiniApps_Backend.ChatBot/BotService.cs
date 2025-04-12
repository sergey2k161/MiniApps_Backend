using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MiniApps_Backend.ChatBot
{
    public class BotService : BackgroundService
    {
        private readonly ITelegramBotClient _botClient;

        public BotService(IConfiguration config)
        {
            _botClient = new TelegramBotClient(config["TelegramBotToken"]);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                cancellationToken: stoppingToken
            );
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            if (update.Type == UpdateType.Message && update.Message?.Text == "/start")
            {
                var handler = new StartCommandHandler(botClient);
                await handler.Handle(update.Message);
            }
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            Console.WriteLine($"Error: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}
