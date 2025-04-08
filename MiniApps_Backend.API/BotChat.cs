//using Telegram.Bot.Polling;
//using Telegram.Bot.Types.Enums;
//using Telegram.Bot;
//using Telegram.Bot.Types;
//using MiniApps_Backend.Business.Services.Interfaces;
//using Telegram.Bot.Types.ReplyMarkups;
//using Telegram.Bot.Exceptions;

//namespace MiniApps_Backend.API
//{
//    public static class BotChat
//    {
//        private static IServiceScopeFactory? _serviceScopeFactory;

//        const string HelpMessage = "Список команд \r\n\r\n/app - Получить приложение\r\n/avgb - Получить ваш средний балл";
//        public static void InitializeTelegramBot(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
//        {
//            _serviceScopeFactory = serviceScopeFactory;

//            var token = configuration["TelegramBotToken"]
//                 ?? throw new ArgumentNullException("TelegramBotToken", "Не указан токен бота в конфигурации");

//            var botClient = new TelegramBotClient(token);

//            var cts = new CancellationTokenSource();

//            // Устанавливаем обработчик обновлений
//            var receiverOptions = new ReceiverOptions
//            {
//                AllowedUpdates = new UpdateType[] { UpdateType.Message }
//            };

//            botClient.StartReceiving(
//                updateHandler: HandleUpdateAsync,
//                errorHandler: HandleErrorAsync,
//                receiverOptions: receiverOptions,
//                cancellationToken: cts.Token);
//        }

//        public static async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
//        {
//            if (update.Message is not { } message || message.From is not { } user) return;

//            if (message.Type == MessageType.Text)
//            {
//                await HandleTextMessageAsync(client, message, user, cancellationToken);
//            }
//            else
//            {
//                await client.SendMessage(
//                    chatId: message.Chat.Id,
//                    text: "Я понимаю только текстовые сообщения.",
//                    cancellationToken: cancellationToken);
//            }
//        }


//        private static async Task HandleTextMessageAsync(ITelegramBotClient client, Message message, Telegram.Bot.Types.User user, CancellationToken cancellationToken)
//        {
//            var chatId = message.Chat.Id;
//            var userId = user.Id;

//            bool isBlocked = await IsUserBlockedAsync(client, userId, chatId, cancellationToken);
//            if (isBlocked)
//            {
//                // Можно записать в лог или просто игнорировать
//                Console.WriteLine($"Пользователь {userId} заблокировал бота.");
//                return;
//            }

//            switch (message.Text)
//            {
//                case "/start":
//                    await SendIntroMessagesAsync(client, chatId, cancellationToken);
//                    break;

//                case "/help":
//                case "ℹ️ Помощь":
//                    await client.SendMessage(chatId, HelpMessage, replyMarkup: GetMainKeyboard(), cancellationToken: cancellationToken);
//                    break;

//                case "/avgb":
//                case "📊 Мой опыт":
//                    using (var scope = _serviceScopeFactory.CreateScope())
//                    {
//                        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
//                        var userExp = await userService.GetUserByTelegramId(user.Id);
//                        var result = userExp == null
//                            ? "Пользователь не найден. Попробуйте еще раз."
//                            : $"Ваш текущий опыт: {userExp.Experience ?? 0} XP. Ваш уровень: {userExp.Level ?? 0}.";

//                        await client.SendMessage(chatId, result, replyMarkup: GetMainKeyboard(), cancellationToken: cancellationToken);
//                    }
//                    break;

//                case "/app":
//                case "📲 MiniApp":
//                    await client.SendMessage(
//                        chatId,
//                        "Нажми на кнопку ниже, чтобы открыть MiniApp 📲",
//                        replyMarkup: GetMiniAppButton(),
//                        cancellationToken: cancellationToken);
//                    break;

//                default:
//                    await client.SendMessage(chatId, "Используйте /start или /help", replyMarkup: GetMainKeyboard(), cancellationToken: cancellationToken);
//                    break;
//            }
//        }

//        private static async Task<bool> IsUserBlockedAsync(ITelegramBotClient client, long userId, long chatId, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var chatMember = await client.GetChatMemberAsync(chatId, userId, cancellationToken);
//                return chatMember.Status == ChatMemberStatus.Kicked;
//            }
//            catch (ApiRequestException ex)
//            {
//                return false;
//            }
//        }

//    }
//}
