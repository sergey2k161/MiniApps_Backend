using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniApps_Backend.Business.Services.Interfaces;
using System.Security.Claims;

namespace MiniApps_Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly ILogger<AnalyticsController> _logger;

        public AnalyticsController(ILogger<AnalyticsController> logger, IAnalyticsService analyticsService)
        {
            _logger = logger;
            _analyticsService = analyticsService;
        }

        /// <summary>
        /// Генерация Excel файла с отчетом по аналитике
        /// </summary>
        /// <param name="accurate">Точный отчет</param>
        /// <returns></returns>
        [Authorize(Roles= "Admin, Analyst")]
        [HttpGet("generate-report")]
        public async Task<IActionResult> GenerateAnalyticsReport([FromQuery] bool accurate)
        {
            _logger.LogInformation($"Пользователь: {User.Identity?.Name}");
            _logger.LogInformation($"Роли: {string.Join(", ", User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value))}");

            try
            {
                var reportData = await _analyticsService.GenerateAnalyticsReport(accurate);
                var fileName = "AnalyticsReport.xlsx";

                return File(reportData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"При создании отчета произошла ошибка: {ex.Message}");
            }
        }
    }
}
