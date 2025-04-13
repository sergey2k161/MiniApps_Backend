using Telegram.Bot;

namespace MiniApps_Backend.Bot.File
{
    public class BotService : IBotService
    {
        private readonly ITelegramBotClient _bot;

        public BotService(ITelegramBotClient bot)
        {
            _bot = bot;
        }

        public async Task ForwardMessageAsync(long userChatId, long sourceChatId, int messageId)
        {
            await _bot.ForwardMessage(
                chatId: userChatId,
                fromChatId: sourceChatId,
                messageId: messageId
            );
        }

        public async Task NotifyInactiveUsersAsync(List<long> telegramId, string message)
        {
            foreach (var id in telegramId)
            {
                try
                {
                    await _bot.SendMessage(
                        chatId: id,
                        text: message
                    );
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка отправки сообщения пользователю {telegramId}: {ex.Message}");
                }
            }
        }

        public async Task SendMessageAsync(long userChatId, string message)
        {
            await _bot.SendMessage(
                chatId: userChatId,
                text: message
            );
        }
    }

}
