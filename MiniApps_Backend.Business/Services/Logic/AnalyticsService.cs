using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity.Analysis;
using MiniApps_Backend.DataBase.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MiniApps_Backend.Business.Services.Logic
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsRepository _analyticsRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDistributedCache _cache;
        private readonly ILogger<AnalyticsService> _logger;

        public AnalyticsService(IAnalyticsRepository analyticsRepository, ILogger<AnalyticsService> logger, IDistributedCache cache, ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _analyticsRepository = analyticsRepository;
            _logger = logger;
            _cache = cache;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public async Task<byte[]> GenerateAnalyticsReport(bool accurate)
        {
            var stopwatch = Stopwatch.StartNew();

            const string reportCacheKey = "analytics_report_cache";

            if (accurate == false)
            {
                var cachedReport = await _cache.GetAsync(reportCacheKey);
                if (cachedReport != null)
                {
                    stopwatch.Stop();
                    _logger.LogInformation($"📄 Excel-отчет получен из кэша за {stopwatch.ElapsedMilliseconds}");
                    return cachedReport;
                }
            }

            var transactions = await _analyticsRepository.GetTransactions();
            var workbook = new XLWorkbook();    
            
            // Страница 1: Прогресс
            await AddProgressAnalyticsSheet(workbook);
            // Страница 2: Популярность тем
            await AddPopularityAnalyticsSheet(workbook);
            // Страница 3: Тесты
            await AddTestAnalyticsSheet(workbook);
            // Страница 4: Бот
            await AddBotAnalyticsSheet(workbook);


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

        public async Task LogActionAsync(string actionName, string result, long userId)
        {
            var action = new BotActionAnalytics
            {
                ActionName = actionName,
                UserId = userId,
                ExecutedAt = DateTime.UtcNow,
                Result = result
            };

            await _analyticsRepository.LogActionAsync(action);
        }

        private async Task AddProgressAnalyticsSheet(XLWorkbook workbook)
        {
            var worksheet1 = workbook.Worksheets.Add("Прогресс");
            worksheet1.Cell(1, 1).Value = "Уникальный Id";
            worksheet1.Cell(1, 2).Value = "Имя";
            worksheet1.Cell(1, 3).Value = "Фамилия";
            worksheet1.Cell(1, 4).Value = "Курс";
            worksheet1.Cell(1, 5).Value = "% завершения курса";
            worksheet1.Cell(1, 6).Value = "Последняя активность";
            worksheet1.Cell(1, 7).Value = "Средняя баллам по тестам";

            worksheet1.Cell(1, 11).Value = "Сводная статистика";
            worksheet1.Cell(2, 10).Value = "Курс";
            worksheet1.Cell(2, 11).Value = "Количество участников курса";
            worksheet1.Cell(2, 12).Value = "Средний % завершения";

            var users = await _userRepository.GetAllUsers();
            var courses = await _courseRepository.GetCourses();

            int row = 2;
            foreach (var user in users)
            {
                foreach (var course in courses)
                {
                    var isSubscribed = await _courseRepository.UserIsSubscribe(user.TelegramId, course.Id);
                    if (!isSubscribed)
                    {
                        continue;
                    }

                    var completionPercentage = await _courseRepository.PercentageCompletionCourse(user.TelegramId, course.Id);
                    var lastActivity = user.LastVisit;
                    var averageTestScore = await _analyticsRepository.AverageTestScore();

                    worksheet1.Cell(row, 1).Value = user.TelegramId;
                    worksheet1.Cell(row, 2).Value = user.FirstName;
                    worksheet1.Cell(row, 3).Value = user.LastName;
                    worksheet1.Cell(row, 4).Value = course.Title;
                    worksheet1.Cell(row, 5).Value = completionPercentage;
                    worksheet1.Cell(row, 6).Value = lastActivity;
                    worksheet1.Cell(2, 7).Value =  averageTestScore;

                    row++;
                }
            }

            // Сводная статистика
            int summaryRow = 3;
            foreach (var course in courses)
            {
                var courseUsers = users.Where(u => _courseRepository.UserIsSubscribe(u.TelegramId, course.Id).Result);
                var totalUsers = courseUsers.Count();

                var averageCompletion = totalUsers > 0
                    ? courseUsers.Average(u => _courseRepository.PercentageCompletionCourse(u.TelegramId, course.Id).Result)
                    : 0;

                worksheet1.Cell(summaryRow, 10).Value = course.Title;
                worksheet1.Cell(summaryRow, 11).Value = totalUsers;
                worksheet1.Cell(summaryRow, 12).Value = Math.Round(averageCompletion, 2);

                summaryRow++;
            }
        }

        private async Task AddPopularityAnalyticsSheet(XLWorkbook workbook)
        {
            var worksheet2 = workbook.Worksheets.Add("Популярность тем");
            worksheet2.Cell(1, 1).Value = "Блок";
            worksheet2.Cell(1, 2).Value = "Количество просмотров";
            worksheet2.Cell(1, 3).Value = "% завершивших блок";
            worksheet2.Cell(1, 4).Value = "% отсева на блоке";

            var visits = await _courseRepository.GetVisitsBlocks();

            var groups = visits
                .GroupBy(v => new
                {
                    v.BlockId,
                    v.BlockTitle
                })
                .ToList();

            var users = await _userRepository.GetUsers();
            

            int row = 2;
            foreach (var group in groups)
            {
                var blockId = group.Key.BlockId;
                var blockTitle = group.Key.BlockTitle;
                int totalViews = group.Count();

                var procenttCompleted = await _courseRepository.PercentageCompletionBlock(blockId);
                var drop = await _courseRepository.PercentageDropoutBlock(blockId);


                // Заполняем строку
                worksheet2.Cell(row, 1).Value = blockTitle;
                worksheet2.Cell(row, 2).Value = totalViews;
                worksheet2.Cell(row, 3).Value = procenttCompleted;
                worksheet2.Cell(row, 4).Value = drop;

                row++;
            }

        }
        
        private async Task AddTestAnalyticsSheet(XLWorkbook workbook)
        {
            var worksheet3 = workbook.Worksheets.Add("Тесты");

            worksheet3.Cell(1, 1).Value = "Уникальный ID";
            worksheet3.Cell(1, 2).Value = "Имя";
            worksheet3.Cell(1, 3).Value = "Фамилия";
            worksheet3.Cell(1, 4).Value = "Тест к блоку"; ;
            worksheet3.Cell(1, 5).Value = "% верно";
            worksheet3.Cell(1, 6).Value = "Попытка";
            worksheet3.Cell(1, 7).Value = "Результат";

            worksheet3.Cell(1, 11).Value = "Сводная статистика";
            worksheet3.Cell(2, 9).Value = "Тест к блоку";
            worksheet3.Cell(2, 10).Value = "Средний % верных ответов";
            worksheet3.Cell(2, 11).Value = "% с первой попытки";
            worksheet3.Cell(2, 12).Value = "% со второй попытки";
            worksheet3.Cell(2, 13).Value = "% с третьей попытки";
            worksheet3.Cell(2, 14).Value = "% более 3 попыток";

            var results = await _courseRepository.GetAllTestResults();

            int row = 2;
            foreach (var result in results)
            {
                worksheet3.Cell(row, 1).Value = result.TelegramId;
                worksheet3.Cell(row, 2).Value = result.RealFirstName;
                worksheet3.Cell(row, 3).Value = result.RealLastName;
                worksheet3.Cell(row, 4).Value = result.BlockName;
                worksheet3.Cell(row, 5).Value = result.PercentageIsTrue;
                worksheet3.Cell(row, 6).Value = result.TryNumber;
                worksheet3.Cell(row, 7).Value = result.Result ? "Сдал" : "Не сдал";
                
                row++;
            }

            // Группируем по блоку
            var groupedByBlock = results.GroupBy(r => r.BlockName);

            int summaryRow = 3; // начинаем ниже заголовков статистики

            foreach (var group in groupedByBlock)
            {
                var blockName = group.Key;
                var totalCount = group.Count();

                // Средний процент правильных ответов
                var avgPercentage = group.Average(r => r.PercentageIsTrue);

                // Кол-во сдавших (Result == true) на конкретную попытку
                int firstTryPass = group.Count(r => r.TryNumber == 1 && r.Result);
                int secondTryPass = group.Count(r => r.TryNumber == 2 && r.Result);
                int thirdTryPass = group.Count(r => r.TryNumber == 3 && r.Result);
                int moreThanThreeTryPass = group.Count(r => r.TryNumber > 3 && r.Result);

                // Считаем процент от общего числа записей в группе
                double percentFirst = totalCount > 0 ? (double)firstTryPass / totalCount * 100 : 0;
                double percentSecond = totalCount > 0 ? (double)secondTryPass / totalCount * 100 : 0;
                double percentThird = totalCount > 0 ? (double)thirdTryPass / totalCount * 100 : 0;
                double percentMore = totalCount > 0 ? (double)moreThanThreeTryPass / totalCount * 100 : 0;

                // Заполняем статистику в Excel
                worksheet3.Cell(summaryRow, 9).Value = blockName;
                worksheet3.Cell(summaryRow, 10).Value = Math.Round(avgPercentage, 2);
                worksheet3.Cell(summaryRow, 11).Value = Math.Round(percentFirst, 2);
                worksheet3.Cell(summaryRow, 12).Value = Math.Round(percentSecond, 2);
                worksheet3.Cell(summaryRow, 13).Value = Math.Round(percentThird, 2);
                worksheet3.Cell(summaryRow, 14).Value = Math.Round(percentMore, 2);

                summaryRow++;
            }
        }

        private async Task AddBotAnalyticsSheet(XLWorkbook workbook)
        {
            var worksheet4 = workbook.Worksheets.Add("Бот");

            var botActions = await _analyticsRepository.GetBotActionAnalytics();

            var actionCounts = botActions
                .GroupBy(action => action.ActionName)
                .Select(group => new
                {
                    ActionName = group.Key,
                    Count = group.Count(),
                    Percentage = Math.Round((double)group.Count() / botActions.Count * 100, 2)
                })
                .ToList();

            int row = 2;
            foreach (var action in actionCounts)
            {
                worksheet4.Cell(row, 1).Value = action.ActionName; 
                worksheet4.Cell(row, 2).Value = action.Count;       
                worksheet4.Cell(row, 3).Value = action.Percentage;  
                row++;
            }

            worksheet4.Cell(1, 1).Value = "Действие (нажатие кнопки/ссылки)";
            worksheet4.Cell(1, 2).Value = "Количество кликов";
            worksheet4.Cell(1, 3).Value = "% от общего числа";
        }
    }
}
