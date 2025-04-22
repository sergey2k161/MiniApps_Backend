using Microsoft.AspNetCore.Mvc;
using MiniApps_Backend.Business.Services.Interfaces;

namespace MiniApps_Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("generate-report")]
        public async Task<IActionResult> GenerateAnalyticsReport([FromQuery] bool accurate)
        {
            try
            {
                var reportData = await _analyticsService.GenerateAnalyticsReport(accurate);
                var fileName = "AnalyticsReport.xlsx";

                return File(reportData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"An error occurred while generating the report: {ex.Message}");
            }
        }
    }
}
