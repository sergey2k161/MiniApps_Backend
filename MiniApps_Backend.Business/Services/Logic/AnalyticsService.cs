using ClosedXML.Excel;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Repositories.Interfaces;
using System.Diagnostics;

namespace MiniApps_Backend.Business.Services.Logic
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsRepository _analyticsRepository;
        private readonly IDistributedCache _cache;
        private readonly ILogger<AnalyticsService> _logger;

        public AnalyticsService(IAnalyticsRepository analyticsRepository, ILogger<AnalyticsService> logger, IDistributedCache cache)
        {
            _analyticsRepository = analyticsRepository;
            _logger = logger;
            _cache = cache;
        }

        public async Task<double> GetPercentageCheating()
        {
            int transactionsCheating = 0;

            var transactions = await _analyticsRepository.GetTransactions();

            _logger.LogInformation($"Количество транзакций для GetPercentageCheating: {transactions.Count}");

            foreach (var transaction in transactions)
            {
                if (transaction.Type)
                {
                    transactionsCheating++;
                }
            }

            if (transactionsCheating == 0)
            {
                return 0;
            }

            var percent = (double)transactionsCheating * 100 / transactions.Count;
            _logger.LogInformation($"Процент: {percent}");


            return Math.Round(percent, 2);
        }

        public async Task<double> GetPercentageEnrollment()
        {
            int transactionsEnrollment = 0;

            var transactions = await _analyticsRepository.GetTransactions();

            foreach (var transaction in transactions)
            {
                if (!transaction.Type)
                {
                    transactionsEnrollment++;
                }
            }

            if (transactionsEnrollment == 0)
            {
                return 0;
            }

            var percent = (double)transactionsEnrollment * 100 / transactions.Count;

            return Math.Round(percent, 2);
        }

        public async Task<byte[]> GenerateAnalyticsReport()
        {
            var stopwatch = Stopwatch.StartNew();

            const string reportCacheKey = "analytics_report_cache";
            var cachedReport = await _cache.GetAsync(reportCacheKey);
            if (cachedReport != null)
            {
                stopwatch.Stop();
                _logger.LogInformation($"📄 Excel-отчет получен из кэша за {stopwatch.ElapsedMilliseconds}");
                return cachedReport;
            }
             
            var transactions = await  _analyticsRepository.GetTransactions();
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Анализ транзакций");

            worksheet.Cell(1, 1).Value = "Тип";
            worksheet.Cell(1, 2).Value = "Сумма";
            worksheet.Cell(1, 4).Value = "Процент пополнений";
            worksheet.Cell(1, 5).Value = "Процент списаний";

            for (int i = 0; i < transactions.Count; i++)
            {
                var transaction = transactions[i];
                worksheet.Cell(i + 2, 1).Value = transaction.Type ? "Пополнение" : "Списание";
                worksheet.Cell(i + 2, 2).Value = transaction.Total;
            }

            worksheet.Cell(2, 4).Value = await GetPercentageCheating();
            worksheet.Cell(2, 5).Value = await GetPercentageEnrollment();

            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            var reportBytes = memoryStream.ToArray();

            await _cache.SetAsync(reportCacheKey, reportBytes, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            stopwatch.Stop();
            _logger.LogInformation($"📤 Excel-отчет сохранен в кэш за {stopwatch.ElapsedMilliseconds}");
            return reportBytes;
        }
    }
}
