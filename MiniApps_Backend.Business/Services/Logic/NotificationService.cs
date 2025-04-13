using Microsoft.Extensions.Logging;
using MiniApps_Backend.Bot;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase;
using MiniApps_Backend.DataBase.Repositories.Interfaces;

namespace MiniApps_Backend.Business.Services.Logic
{
    public class NotificationService : INotificationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBotService _botService;
        private readonly MaDbContext _context;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IUserRepository userRepository, IBotService botService, MaDbContext context, ILogger<NotificationService> logger)
        {
            _userRepository = userRepository;
            _botService = botService;
            _context = context;
            _logger = logger;
        }

        public async Task SendNotificationAsync(long telegramId, string message)
        {
            await _botService.SendMessageAsync(telegramId, message);
        }

        public async Task SendNotificationsToAllAsync(string message)
        {
            var users = await _userRepository.GetAllUsers();

            foreach (var user in users)
            {
                await SendNotificationAsync(user.TelegramId, message);
            }
        }

        public async Task CheckAndSendNotificationsAsync()
        {
            _logger.LogInformation("Запуск проверки уведомлений: {Time}", DateTime.UtcNow);

            var users = await _userRepository.GetUsersForNotification();
            int sentCount = 0;

            foreach (var user in users)
            {
                try
                {
                    var nextNotificationDate = user.LastNotification.AddDays(user.NotificationFrequency);

                    if (DateTime.UtcNow >= nextNotificationDate && DateTime.UtcNow >= user.LastVisit.AddDays(3))
                    {
                        await SendNotificationAsync(user.TelegramId, "Э не будь чертом, реши задачки а");
                        user.LastNotification = DateTime.UtcNow;
                        sentCount++;

                        _logger.LogInformation("Отправлено уведомление пользователю {UserId} (TelegramId: {TelegramId})", user.Id, user.TelegramId);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при отправке уведомления пользователю {UserId}", user.Id);
                }
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("Отправлено уведомлений: {Count}", sentCount);
        }

    }
}
