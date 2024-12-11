using ATM.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATM.UI.Pages;

public class WithdrawModel : PageModel
{
    [BindProperty]
    public decimal Amount { get; set; }

    public string? ErrorMessage { get; set; }

    private readonly Withdraw _withdraw;

    public WithdrawModel(Withdraw withdraw)
    {
        _withdraw = withdraw;
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
            await _withdraw.ExecuteAsync(Amount);
            TempData["Message"] = "Ya puede retirar su dinero.";
            return RedirectToPage("/Menu");
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            ModelState.AddModelError("", $"Hubo un error procesando su retiro: {ex.Message}.");
            ModelState.AddModelError("", "Intentelo nuevamente.");
        }

        return Page(); // Devuelve la misma página para mostrar el resultado
    }
}
