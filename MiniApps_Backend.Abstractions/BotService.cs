using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace MiniApps_Backend.Bot.File
{
    /// <summary>
    /// Сервис для взаимодействия с Telegram Bot API.
    /// </summary>
    public class BotService : IBotService
    {
        private readonly ITelegramBotClient _bot;
        private readonly ILogger<BotService> _logger;

        public BotService(ITelegramBotClient bot, ILogger<BotService> logger)
        {
            _bot = bot;
            _logger = logger;
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
            var errorMessage = 43;
            try
            {
                await _bot.ForwardMessage(
                    chatId: userChatId,
                    fromChatId: sourceChatId,
                    messageId: messageId);

                _logger.LogInformation($"Сообщение успешно отправлено пользователю {userChatId}");
            }
            catch (Exception ex)
            {
                await _bot.ForwardMessage(
                    chatId: userChatId,
                    fromChatId: sourceChatId,
                    messageId: errorMessage);

                _logger.LogWarning($"Сообщение не отправлено из-за ошибки {ex}");
            }
            
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
