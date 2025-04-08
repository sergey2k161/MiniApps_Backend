using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace MiniApps_Backend.ChatBot
{
    public class StartCommandHandler
    {
        private readonly ITelegramBotClient _botClient;

        public StartCommandHandler(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task Handle(Message message)
        {
            var chatId = message.Chat.Id;

            var keyboard = new InlineKeyboardMarkup(
                InlineKeyboardButton.WithWebApp("Открыть MiniApp", new WebAppInfo
                {
                    Url = "https://your-mini-app-url.com"
                })
            );

            await _botClient.SendTextMessageAsync(
                chatId,
                "Добро пожаловать! Нажмите кнопку ниже 👇",
                replyMarkup: keyboard
            );
        }
    }

}
