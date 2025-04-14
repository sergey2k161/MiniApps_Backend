namespace MiniApps_Backend.Bot
{
    public interface IBotService
    {
        Task ForwardMessageAsync(long userChatId, long sourceChatId, int messageId);

        Task SendMessageAsync(long userChatId, string message);

        Task NotifyInactiveUsersAsync(List<long> telegramId, string message);
    }
}
