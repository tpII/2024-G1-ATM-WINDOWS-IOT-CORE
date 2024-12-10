using ATM.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATM.UI.Pages;

public class DepositModel : PageModel
{
    private readonly Deposit _deposit;

    public DepositModel(Deposit deposit)
    {
        _deposit = deposit;
    }

    [BindProperty]
    public string AccountId { get; set; }

    [BindProperty]
    public decimal Amount { get; set; }

    public string? SuccessMessage { get; set; }
    public string? ErrorMessage { get; set; }

    // Este método se ejecuta al enviar el formulario
    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(AccountId) || Amount <= 0)
        {
            ErrorMessage = "Account ID and amount must be valid.";
            return Page(); // Devuelve la misma página para mostrar el error
        }

        try
        {
            await _deposit.ExecuteAsync(AccountId, Amount); // Llama al caso de uso para realizar el depósito
            SuccessMessage = $"Successfully deposited {Amount:C} to account {AccountId}.";
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }

        return Page(); // Devuelve la misma página para mostrar el resultado
    }
}
