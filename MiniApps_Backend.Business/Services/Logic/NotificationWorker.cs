using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MiniApps_Backend.Business.Services.Logic
{
    public class NotificationWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var notificationService = scope.ServiceProvider.GetRequiredService<NotificationService>();
                    await notificationService.CheckAndSendNotificationsAsync();
                }

                await Task.Delay(TimeSpan.FromHours(4), stoppingToken);
                //await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            }
        }
    }
}
