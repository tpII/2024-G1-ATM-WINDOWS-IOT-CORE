using ATM.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ATM.UI.Controllers;

[ApiController]
[Route("api/session")]
public class SessionController : ControllerBase
{
    private readonly SessionService _sessionService;

    public SessionController(SessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpPost("timeout")]
    public IActionResult HandleTimeout()
    {
        // Aquí puedes agregar lógica, como registrar el evento de tiempo de espera
        _sessionService.LogOut();
        return Ok(new { message = "Sesión cerrada por inactividad" });
    }
}
