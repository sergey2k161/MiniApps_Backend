namespace MiniApps_Backend.Business.Services.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(long telegramId, string message);

        Task SendNotificationsToAllAsync(string message);
    }
}
