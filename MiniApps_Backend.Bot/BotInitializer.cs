using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using MiniApps_Backend.Business.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using MiniApps_Backend.Bot.Handlers;
using Microsoft.Extensions.DependencyInjection;
using MiniApps_Backend.DataBase.Models.Dto;
using Telegram.Bot.Requests.Abstractions;

namespace MiniApps_Backend.Bot
{
    public class BotInitializer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITelegramBotClient _botClient;
        private static readonly Dictionary<long, string> _userStates = new();

        public BotInitializer(IServiceProvider serviceProvider, ITelegramBotClient botClient)
        {
            _serviceProvider = serviceProvider;
            _botClient = botClient;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new[] { UpdateType.Message }
            };

            var cts = new CancellationTokenSource();

            // Передаем методы с правильной сигнатурой
            _botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                errorHandler: HandleErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token);

            Console.WriteLine("🤖 Бот запущен.");
            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message || message.From is not { } user) return;

            if (message.Type == MessageType.Text)
            {
                await HandleTextMessageAsync(client, message, user, cancellationToken);
            }
            else
            {
                await client.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Я понимаю только текстовые сообщения.",
                    cancellationToken: cancellationToken);
            }
        }

        public async Task HandleTextMessageAsync(ITelegramBotClient client, Message message, Telegram.Bot.Types.User user, CancellationToken cancellationToken)
        {
            var chatId = message.Chat.Id;
            var userId = user.Id;

            using var scope = _serviceProvider.CreateScope();
            var _userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            // Если пользователь вводит email (по состоянию)
            if (_userStates.TryGetValue(chatId, out var state) && state == "awaiting_email")
            {
                var email = message.Text;

                if (!string.IsNullOrEmpty(email) && email.Contains("@"))
                {
                    var userRequest = new UserRequest
                    {
                        TelegramId = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.Username,
                        Email = email
                    };

                    await _userService.AddUser(userRequest);
                    await client.SendMessage(chatId, "Спасибо! ✅ Email сохранён. Теперь можешь пользоваться ботом.", cancellationToken: cancellationToken);

                    _userStates.Remove(chatId);
                }
                else
                {
                    await client.SendMessage(chatId, "Пожалуйста, введите корректный Email 📧", cancellationToken: cancellationToken);
                }

                return; 
            }

            // Стандартная обработка команд
            switch (message.Text)
            {
                case "/start":
                    await BotMenu.SendIntroMessagesAsync(client, chatId, cancellationToken);
                    _userStates[chatId] = "awaiting_email"; 
                    break;

                case "/app":
                case "📲 MiniApp":
                    await client.SendMessage(chatId, "Нажми на кнопку ниже, чтобы открыть MiniApp 📲", replyMarkup: BotMenu.GetMiniAppButton(), cancellationToken: cancellationToken);
                    break;

                case "/exp":
                case "📊 Мой опыт и уровень":
                    var userDb = await _userService.GetUserByTelegramId(userId);
                    await client.SendMessage(chatId, $"Ваш уровень: {userDb.Level}, опыт: {userDb.Experience}", cancellationToken: cancellationToken);
                    break;

                case "/help":
                case "ℹ️ Помощь":
                    await client.SendMessage(chatId, "Список команд: \n/app - MiniApp\n/help - список команд", cancellationToken: cancellationToken);
                    break;

                default:
                    await client.SendMessage(chatId, "Моя твоя не понимать пиши /help", replyMarkup: BotMenu.GetMainKeyboard(), cancellationToken: cancellationToken);
                    break;
            }
        }

        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Ошибка: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}
