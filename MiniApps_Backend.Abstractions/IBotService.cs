namespace MiniApps_Backend.Bot
{
    public interface IBotService
    {
        Task ForwardMessageAsync(long userChatId, long sourceChatId, int messageId);
    }
}
