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
    public decimal? Balance { get; set; }
    public string? ErrorMessage { get; set; }

    // Este método se ejecuta cuando se envía el formulario
    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            Balance = await _checkBalance.ExecuteAsync(); // Llama al caso de uso para obtener el balance
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error al obtener balance: " + ex.Message;
        }

        return Page(); // Devuelve la misma página para mostrar el resultado
    }
}