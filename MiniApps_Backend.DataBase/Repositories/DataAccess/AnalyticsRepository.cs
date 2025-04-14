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

        public async Task<List<LessonResult>> GetLessonResults()
        {
            return await _context.LessonResults.ToListAsync();
        }

        public async Task<List<TestResult>> GetTestResults()
        {
            return await _context.TestResults.ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }
    }
}
