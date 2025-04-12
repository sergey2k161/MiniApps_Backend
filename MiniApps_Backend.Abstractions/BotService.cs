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
    }

}
