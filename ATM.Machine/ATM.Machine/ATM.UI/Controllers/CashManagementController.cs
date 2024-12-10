using Microsoft.AspNetCore.Http;
using ATM.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ATM.UI.Controllers
{
    [ApiController]
    [Route("api/cash-management")]
    public class CashManagementController : ControllerBase
    {
        private readonly ICashManagementService _cashManagementService;

        public CashManagementController(ICashManagementService cashManagementService)
        {
            _cashManagementService = cashManagementService;
        }

        // Endpoint para obtener el efectivo disponible
        [HttpGet("available-cash")]
        public IActionResult GetAvailableCash()
        {
            var availableCash = _cashManagementService.GetAvailableCash();
            return Ok(new { availableCash });
        }

        // Endpoint para cargar efectivo
        [HttpPost("load-cash")]
        public IActionResult LoadCash([FromBody] int amount)
        {
            _cashManagementService.LoadCash(amount);
            return Ok(new { message = "Cash loaded successfully", loadedAmount = amount });
        }
    }
}

