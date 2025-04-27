namespace MiniApps_Backend.Business.Services.Interfaces
{
    /// <summary>
    /// Интерфейс для сервиса аналитики
    /// </summary>
    public interface IAnalyticsService
    {
        /// <summary>
        /// Генерация отчета по аналитике
        /// </summary>
        /// <param name="accurate"></param>
        /// <returns></returns>
        Task<byte[]> GenerateAnalyticsReport(bool accurate);

        /// <summary>
        /// Логирование действий пользователя
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="result"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task LogActionAsync(string actionName, string result, long userId);
    }
}
