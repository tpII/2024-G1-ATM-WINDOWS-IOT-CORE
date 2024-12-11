using ATM.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ATM.Application.Services;

namespace ATM.UI.Pages;

public class WithdrawModel : PageModel
{
    [BindProperty]
    public decimal Amount { get; set; }

    public string? ErrorMessage { get; set; }

    public SessionService SessionService { get; set; }

    private readonly Withdraw _withdraw;

    public WithdrawModel(Withdraw withdraw, SessionService sessionService)
    {
        _withdraw = withdraw;
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
            ErrorMessage = "El monto debe ser mayor a cero.";
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
        }

        return Page(); // Devuelve la misma página para mostrar el resultado
    }
}
