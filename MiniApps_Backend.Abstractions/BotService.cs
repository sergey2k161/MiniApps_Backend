using Telegram.Bot;

namespace MiniApps_Backend.Bot.File
{
    /// <summary>
    /// Сервис для взаимодействия с Telegram Bot API.
    /// </summary>
    public class BotService : IBotService
    {
        private readonly ITelegramBotClient _bot;

        public BotService(ITelegramBotClient bot)
        {
            _bot = bot;
        }

        /// <summary>
        /// Пересылает сообщение из одного чата в другой
        /// </summary>
        /// <param name="userChatId">ID чата пользователя, которому будет переслано сообщение</param>
        /// <param name="sourceChatId">ID чата, из которого будет переслано сообщение</param>
        /// <param name="messageId">ID пересылаемого сообщения</param>
        /// <returns></returns>
        public async Task ForwardMessageAsync(long userChatId, long sourceChatId, int messageId)
        {
            await _bot.ForwardMessage(
                chatId: userChatId,
                fromChatId: sourceChatId,
                messageId: messageId
            );
        }

        /// <summary>
        /// Отправляет уведомление списку неактивных пользователей
        /// </summary>
        /// <param name="telegramId">Список Telegram ID пользователей, которым будет отправлено сообщение</param>
        /// <param name="message">Текст сообщения для отправки.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Отправляет сообщение конкретному пользователю
        /// </summary>
        /// <param name="userChatId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(long userChatId, string message)
        {
            await _bot.SendMessage(
                chatId: userChatId,
                text: message
            );
        }
    }
}
