using System;
using Microsoft.AspNetCore.Mvc;

namespace ATM.UI.Controllers;

[ApiController]
[Route("api/cards")]
public class CardController : ControllerBase
{

    [HttpPost("detected")]
    public IActionResult CardDetected([FromBody] CardNotification notification)
    {
        if (notification?.CardId == null)
        {
            return BadRequest("Card ID is required.");
        }

        Console.WriteLine($"Card detected: {notification.CardId}");
        // Procesa la tarjeta detectada
        return Ok();
    }

    [HttpPost("test")]
    public IActionResult CardTest([FromBody] string? test)
    {
        if (test == null)
        {
            return BadRequest("test is null.");
        }

        Console.WriteLine($"Test: {test}");
        // Procesa la tarjeta detectada
        return Ok();
    }

}

public class CardNotification
{
    public string? CardId { get; set; }
}
