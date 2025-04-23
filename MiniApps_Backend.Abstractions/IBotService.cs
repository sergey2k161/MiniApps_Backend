namespace MiniApps_Backend.Bot
{
    public interface IBotService
    {
        /// <summary>
        /// Асинхронно пересылает сообщение от одного чата к другому
        /// </summary>
        /// <param name="userChatId">Идентификатор чата пользователя, куда пересылается сообщение</param>
        /// <param name="sourceChatId">Идентификатор исходного чата, откуда берется сообщение</param>
        /// <param name="messageId">Идентификатор сообщения для пересылки</param>
        /// <returns></returns>
        Task ForwardMessageAsync(long userChatId, long sourceChatId, int messageId);

        /// <summary>
        /// Асинхронно отправляет сообщение пользователю
        /// </summary>
        /// <param name="userChatId">Идентификатор чата пользователя, которому отправляется сообщение</param>
        /// <param name="message">Текст сообщения для отправки</param>
        /// <returns></returns>
        Task SendMessageAsync(long userChatId, string message);

        /// <summary>
        /// Уведомляет неактивных пользователей, отправляя им сообщение
        /// </summary>
        /// <param name="telegramId">Список идентификаторов Telegram пользователей</param>
        /// <param name="message">Текст уведомления для отправки</param>
        /// <returns></returns>
        Task NotifyInactiveUsersAsync(List<long> telegramId, string message);
    }
}
