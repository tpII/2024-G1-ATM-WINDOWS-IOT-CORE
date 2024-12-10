using ATM.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATM.UI.Pages;

public class WithdrawModel : PageModel
{
    private readonly Withdraw _withdraw;

    public WithdrawModel(Withdraw withdraw)
    {
        _withdraw = withdraw;
    }

    [BindProperty]
    public decimal Amount { get; set; }

    public string? SuccessMessage { get; set; }
    public string? ErrorMessage { get; set; }

    // Este método se ejecuta al enviar el formulario
    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            await _withdraw.ExecuteAsync(Amount);
            SuccessMessage = $"Successfully withdrawed {Amount:C}.";
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }

        return Page(); // Devuelve la misma página para mostrar el resultado
    }
}
