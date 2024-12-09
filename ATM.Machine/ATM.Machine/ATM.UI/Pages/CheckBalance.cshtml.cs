using ATM.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATM.UI.Pages;

public class CheckBalanceModel : PageModel
{
    private readonly CheckBalance _checkBalance;

    public CheckBalanceModel(CheckBalance checkBalance)
    {
        _checkBalance = checkBalance;
    }

    [BindProperty]
    public string AccountId { get; set; }
    public decimal? Balance { get; set; }
    public string? ErrorMessage { get; set; }

    // Este método se ejecuta cuando se envía el formulario
    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(AccountId))
        {
            ErrorMessage = "Account ID cannot be empty.";
            return Page(); // Devuelve la misma página para mostrar el error
        }

        try
        {
            Balance = await _checkBalance.ExecuteAsync(AccountId); // Llama al caso de uso para obtener el balance
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }

        return Page(); // Devuelve la misma página para mostrar el resultado
    }
}