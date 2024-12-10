using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ATM.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ATM.UI.Controllers
{
    [ApiController]
    [Route("api/atm/configuration")]
    public class ATMConfigurationController : ControllerBase
    {
        private readonly IATMConfigurationService _service;

        public ATMConfigurationController(IATMConfigurationService service)
        {
            _service = service;
        }

        [HttpPost("set-limits")]
        public async Task<IActionResult> SetWithdrawalLimits([FromBody] SetWithdrawalLimitsRequest request)
        {
            await _service.SetWithdrawalLimitsAsync(request.MaxLimit, request.MinLimit);
            return Ok();
        }

        [HttpGet("get-limits")]
        public async Task<IActionResult> GetWithdrawalLimits()
        {
            var limits = await _service.GetWithdrawalLimitsAsync();
            return Ok(limits);
        }

        public class SetWithdrawalLimitsRequest
        {
            public int MaxLimit { get; set; }
            public int MinLimit { get; set; }
        }
    }
}


