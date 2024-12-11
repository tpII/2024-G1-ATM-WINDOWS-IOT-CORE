using System.Threading.Tasks;
using ATM.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using ATM.Application.UseCases;
using Microsoft.AspNetCore.SignalR;
using ATM.Infrastructure.Hubs;
using ATM.Application.Services;

namespace ATM.UI.ApiControllers
{
    [ApiController]
    [Route("api/cash-management")]
    public class CashManagementController : ControllerBase
    {
        private readonly CashManagementService _service;

        private readonly IHubContext<CardNotificationHub> _hubContext;

        private readonly EnterCardUseCase _useCase;

        public CashManagementController(CashManagementService service, EnterCardUseCase useCase, IHubContext<CardNotificationHub> hubContext)
        {
            _hubContext = hubContext;
            _service = service;
            _useCase = useCase;
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

        // [HttpPost("test")]
        // public async Task<IActionResult> Test([FromBody] string number)
        // {
        //     if (string.IsNullOrWhiteSpace(number))
        //     {
        //         return BadRequest("Number cannot be null or empty.");
        //     }

        //     bool success = await _useCase.ExecuteAsync(number);
        //     await _hubContext.Clients.All.SendAsync("CardDetected", success);

        //     return success ? Ok() : BadRequest();
        // }

    }
}