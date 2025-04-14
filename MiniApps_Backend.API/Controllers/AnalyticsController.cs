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

        [HttpGet("percent-cheating")]
        public async Task<IActionResult> GetPercentageCheating()
        {
            var result = await _analyticsService.GetPercentageCheating();

            return Ok(result);
        }
        
        [HttpGet("percent-enrollment")]
        public async Task<IActionResult> GetPercentageEnrollment()
        {
            var result = await _analyticsService.GetPercentageEnrollment();

            return Ok(result);
        }

        [HttpGet("generate-report")]
        public async Task<IActionResult> GenerateAnalyticsReport()
        {
            try
            {
                var reportData = await _analyticsService.GenerateAnalyticsReport();
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
