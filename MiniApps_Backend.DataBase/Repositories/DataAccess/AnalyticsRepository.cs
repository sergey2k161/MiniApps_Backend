using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.Ammount;
using MiniApps_Backend.DataBase.Repositories.Interfaces;


namespace MiniApps_Backend.DataBase.Repositories.DataAccess
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly MaDbContext _context;

        public AnalyticsRepository(MaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение всех результатов уроков
        /// </summary>
        /// <returns></returns>
        public async Task<List<LessonResult>> GetLessonResults()
        {
            return await _context.LessonResults.ToListAsync();
        }

        /// <summary>
        /// Получение всех результатов тестов
        /// </summary>
        /// <returns></returns>
        public async Task<List<TestResult>> GetTestResults()
        {
            return await _context.TestResults.ToListAsync();
        }

        /// <summary>
        /// Получение всех транзакций
        /// </summary>
        /// <returns></returns>
        public async Task<List<Transaction>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }
    }
}
