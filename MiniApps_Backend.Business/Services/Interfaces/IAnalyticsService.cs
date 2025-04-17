using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.Ammount;

namespace MiniApps_Backend.Business.Services.Interfaces
{
    public interface IAnalyticsService
    {
        // Транзакции

        /// <summary>
        /// Процент зачислений
        /// </summary>
        /// <returns>%</returns>
        Task<double> GetPercentageEnrollment();

        /// <summary>
        /// Процент списаний
        /// </summary>
        /// <returns>%</returns>
        Task<double> GetPercentageCheating();

        Task<byte[]> GenerateAnalyticsReport(bool accurate);

        //Task<List<LessonResult>> GetLessonResults();

        //Task<List<Transaction>> GetTransactions();
    }
}
