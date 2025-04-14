using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.Ammount;

namespace MiniApps_Backend.DataBase.Repositories.Interfaces
{
    public interface IAnalyticsRepository
    {
        Task<List<TestResult>> GetTestResults();

        Task<List<LessonResult>> GetLessonResults();

        Task<List<Transaction>> GetTransactions();

    }
}
