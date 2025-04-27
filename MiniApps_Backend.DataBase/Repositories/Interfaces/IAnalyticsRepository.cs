using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.Ammount;
using MiniApps_Backend.DataBase.Models.Entity.Analysis;

namespace MiniApps_Backend.DataBase.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для работы с аналитикой
    /// </summary>
    public interface IAnalyticsRepository
    {
        /// <summary>
        /// Получение результатов уроков
        /// </summary>
        /// <returns></returns>
        Task<List<LessonResult>> GetLessonResults();

        /// <summary>
        /// Получение транзакций
        /// </summary>
        /// <returns></returns>
        Task<List<Transaction>> GetTransactions();

        /// <summary>
        /// Логировние действий бота
        /// </summary>
        /// <param name="botActionAnalytics"></param>
        /// <returns></returns>
        Task LogActionAsync(BotActionAnalytics botActionAnalytics);

        /// <summary>
        /// Получение всех действий бота
        /// </summary>
        /// <returns></returns>
        Task<List<BotActionAnalytics>> GetBotActionAnalytics();

        /// <summary>
        /// Получение всех действий бота
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetDistinctActions();

        Task<List<User>> GetAnalyticsForUsers();

        Task<double> AverageTestScore();
    }
}
