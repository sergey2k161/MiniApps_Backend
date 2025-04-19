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

        /// <summary>
        /// Отправка уведомления пользователю
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendNotificationAsync(long telegramId, string message)
        {
            await _botService.SendMessageAsync(telegramId, message);
        }

        /// <summary>
        /// Отправка уведомления всем пользователям
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendNotificationsToAllAsync(string message)
        {
            var users = await _userRepository.GetAllUsers();

            foreach (var user in users)
            {
                await SendNotificationAsync(user.TelegramId, message);
            }
        }

        /// <summary>
        /// Проверка пользователей на необходимость отправки уведомлений
        /// </summary>
        /// <returns></returns>
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

                    if (DateTime.UtcNow >= nextNotificationDate && DateTime.UtcNow >= user.LastVisit.AddDays(1))
                    {
                        var random = new Random();
                        var inactivityMessages = new[]
                        {
                            "Привет! Мы заметили, что ты давно не заходил. Не забывай продолжать обучение, чтобы достичь своих целей!",
                            "Давно не виделись! Твои курсы ждут тебя. Возвращайся и продолжай учиться!",
                            "Мы скучаем по тебе! Заходи, чтобы продолжить свои курсы и узнать что-то новое.",
                            "Не забывай про свои цели! Курсы ждут тебя, чтобы помочь их достичь.",
                            "Привет! Напоминаем, что обучение — это ключ к успеху. Возвращайся к своим курсам!",
                            "Давно не было активности. Не упусти возможность продолжить обучение и улучшить свои навыки.",
                            "Твои курсы скучают по тебе! Заходи и продолжай свой путь к знаниям.",
                            "Не останавливайся на достигнутом! Возвращайся к обучению и достигай новых высот.",
                            "Привет! Мы заметили, что ты давно не заходил. Самое время вернуться и продолжить обучение!"
                        };
                        await SendNotificationAsync(user.TelegramId, inactivityMessages[random.Next(inactivityMessages.Length)]);
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
