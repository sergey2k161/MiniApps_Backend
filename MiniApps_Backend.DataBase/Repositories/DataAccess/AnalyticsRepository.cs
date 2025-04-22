using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.Ammount;
using MiniApps_Backend.DataBase.Models.Entity.Analysis;
using MiniApps_Backend.DataBase.Repositories.Interfaces;


namespace MiniApps_Backend.DataBase.Repositories.DataAccess
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly MaDbContext _context;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;


        public AnalyticsRepository(MaDbContext context, ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _context = context;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public async Task<double> AverageTestScore()
        {
            var testResults = await _courseRepository.GetAllTestResults();
            var avg = testResults.Average(x => x.PercentageIsTrue);

            return Math.Round(avg, 2);
        }

        public async Task<List<User>> GetAnalyticsForUsers()
        {
            return await _context.Users
                .Include(u => u.Courses) 
                .Where(u => u.Courses.Any()) 
                .ToListAsync();
        }

        public async Task<List<BotActionAnalytics>> GetBotActionAnalytics()
        {
            return await _context.BotActionsAnalytics.ToListAsync();
        }

        public async Task<List<string>> GetDistinctActions()
        {
            var uniqueActionNames = await _context.BotActionsAnalytics
                .Select(x => x.ActionName)
                .Distinct()
                .ToListAsync();

            return uniqueActionNames;
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
        /// Получение всех транзакций
        /// </summary>
        /// <returns></returns>
        public async Task<List<Transaction>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task LogActionAsync(BotActionAnalytics botActionAnalytics)
        {
            await _context.BotActionsAnalytics.AddAsync(botActionAnalytics);
            await _context.SaveChangesAsync();
        }
    }
}
