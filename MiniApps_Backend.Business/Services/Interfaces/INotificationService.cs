namespace MiniApps_Backend.Business.Services.Interfaces
{
    /// <summary>
    /// Интерфейс для сервиса уведомлений
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Отправка уведомления пользователю
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendNotificationAsync(long telegramId, string message);

        /// <summary>
        /// Отправка уведомления всем пользователям
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendNotificationsToAllAsync(string message);
    }
}
