using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.Ammount;
using MiniApps_Backend.DataBase.Models.Entity.Analysis;

namespace MiniApps_Backend.DataBase.Repositories.Interfaces
{
    public interface IAnalyticsRepository
    {
        //Task<List<TestResultDto>> GetTestResults();

        Task<List<LessonResult>> GetLessonResults();

        Task<List<Transaction>> GetTransactions();

        Task LogActionAsync(BotActionAnalytics botActionAnalytics);

        Task<List<BotActionAnalytics>> GetBotActionAnalytics();

        Task<List<string>> GetDistinctActions();

        Task<List<User>> GetAnalyticsForUsers();

        Task<double> AverageTestScore();
    }
}
