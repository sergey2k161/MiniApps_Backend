using Microsoft.AspNetCore.Mvc;
using MiniApps_Backend.Business.Services.Interfaces;

namespace MiniApps_Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupportsController : ControllerBase
    {
        private readonly ISupportService _supportService;

        public SupportsController(ISupportService supportService)
        {
            _supportService = supportService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupport(long telegramId, string message)
        {
            var result = await _supportService.CreateSupport(telegramId, message);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetSupports()
        {
            var result = await _supportService.GetSupports();

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpPatch("take-appeal")]
        public async Task<IActionResult> TakeAppeal(Guid id, long helper)
        {
            var result = await _supportService.TakeAppeal(id, helper);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPatch("change-helper")]
        public async Task<IActionResult> ChangeHelper(Guid id, long helper)
        {
            var result = await _supportService.ChangeHelper(id, helper);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPatch("change-process")]
        public async Task<IActionResult> ChangeProcess(Guid id, string process)
        {
            var result = await _supportService.ChangeProcess(id, process);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPatch("change-status")]
        public async Task<IActionResult> ChangeStatus(Guid id, string newStatus)
        {
            var result = await _supportService.ChangeStatus(id, newStatus);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupportById(Guid id)
        {
            var result = await _supportService.GetSupportById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
