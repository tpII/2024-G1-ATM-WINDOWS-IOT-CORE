using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ATM.Application.Services;
using ATM.Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace ATM.UI.Controllers
{
    [ApiController]
    [Route("api/config")]
    public class ATMConfigurationController : ControllerBase
    {
        private readonly ATMConfigurationService _service;

        public ATMConfigurationController(ATMConfigurationService service)
        {
            _service = service;
        }

        [HttpGet("get-status")]
        public IActionResult GetStatus()
        {
            return Ok();
        }

        [HttpPost("set-limits")]
        public async Task<IActionResult> SetWithdrawalLimits([FromBody] SetWithdrawalLimitsRequest request)
        {
            try
            {
                await _service.SetWithdrawalLimitsAsync(request.MaxLimit, request.MinLimit);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        [HttpGet("get-limits")]
        public async Task<IActionResult> GetWithdrawalLimits()
        {
            var limits = await _service.GetWithdrawalLimitsAsync();

            var response = new
            {
                MaxLimit = limits.MaxLimit,
                MinLimit = limits.MinLimit
            };

            return Ok(response);
        }

        public class SetWithdrawalLimitsRequest
        {
            public int MaxLimit { get; set; }
            public int MinLimit { get; set; }
        }
    }
}


