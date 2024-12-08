using ATM.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATM.UI.Pages;

public class CheckBalanceModel : PageModel
{
    private readonly CheckBalance _checkBalanceUseCase;

    public CheckBalanceModel(CheckBalance checkBalanceUseCase)
    {
        _checkBalanceUseCase = checkBalanceUseCase;
    }

    [BindProperty]
    public string? AccountId { get; set; }

    public decimal? Balance { get; private set; }
    public string? ErrorMessage { get; private set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(AccountId))
        {
            ErrorMessage = "Account ID is required.";
            return Page();
        }

        try
        {
            Balance = await _checkBalanceUseCase.ExecuteAsync(AccountId);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }

        return Page();
    }
}
