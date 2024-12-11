using ATM.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ATM.Application.Services;

namespace ATM.UI.Pages;

public class CheckBalanceModel : PageModel
{
    private readonly CheckBalance _checkBalance;

    public decimal? Balance { get; set; }

    public string? ErrorMessage { get; set; }

    public SessionService SessionService { get; set; }

    public CheckBalanceModel(CheckBalance checkBalance, SessionService sessionService)
    {
        _checkBalance = checkBalance;
        SessionService = sessionService;
    }

    // Este método se ejecuta cuando se envía el formulario
    public async Task<IActionResult> OnGetAsync()
    {
        if(SessionService.IsSessionActive())
        {
            TempData["Message"] = "Por favor acerque su tarjeta para ingresar al sistema";
            return RedirectToPage("/Index");
        }
        try
        {
            Balance = await _checkBalance.ExecuteAsync(); // Llama al caso de uso para obtener el balance
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }

        return Page(); // Devuelve la misma página para mostrar el resultado
    }
}