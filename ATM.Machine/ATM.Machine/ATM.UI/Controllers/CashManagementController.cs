using System.Threading.Tasks;
using ATM.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ATM.UI.ApiControllers
{
    [ApiController]
    [Route("api/cash-management")]
    public class CashManagementController : ControllerBase
    {
        private readonly ICashManagementService _service;

        public CashManagementController(ICashManagementService service)
        {
            _service = service;
        }

        [HttpGet("available-cash")]
        public async Task<IActionResult> GetAvailableCash()
        {
            var cash = await _service.GetAvailableCashAsync();
            return Ok(cash);
        }

        [HttpPost("load-cash")]
        public async Task<IActionResult> LoadCash([FromBody] int amount)
        {
            await _service.LoadCashAsync(amount);
            return Ok(new { Message = "Cash loaded successfully" });
        }
    }
}
