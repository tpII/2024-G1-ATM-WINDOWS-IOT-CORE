using ATM.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ATM.Application.Services;

namespace ATM.UI.Pages;

public class DepositModel : PageModel
{
    [BindProperty]
    public decimal Amount { get; set; }

    private readonly Deposit _deposit;

    public string? ErrorMessage { get; set; }

    public SessionService SessionService { get; set; }

    public DepositModel(Deposit deposit, SessionService sessionService)
    {
        _deposit = deposit;
        SessionService = sessionService;
    }

    public IActionResult OnGet()
    {
        if(SessionService.IsSessionActive())
        {
            TempData["Message"] = "Por favor acerque su tarjeta para ingresar al sistema";
            return RedirectToPage("/Index");
        }
        return Page();
    }

    // Este método se ejecuta al enviar el formulario
    public async Task<IActionResult> OnPostAsync()
    {
        if (Amount <= 0)
        {
            ModelState.AddModelError("", "El monto debe ser mayor a cero.");
            return Page();
        }

        try
        {
            await _deposit.ExecuteAsync(Amount); // Llama al caso de uso para realizar el depósito
            TempData["Message"] = "Depósito realizado con éxito.";
            return RedirectToPage("/Menu");
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message + $" {Amount}";
            ModelState.AddModelError("", $"Hubo un error procesando su depósito: {ex.Message}.");
            ModelState.AddModelError("", "Intentelo nuevamente.");
        }

        return Page(); // Devuelve la misma página para mostrar el resultado
    }
}