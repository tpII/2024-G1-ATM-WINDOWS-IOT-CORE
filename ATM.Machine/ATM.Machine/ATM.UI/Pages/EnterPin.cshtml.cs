using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ATM.Application.Interfaces.Services;
using ATM.Application.Services;
using ATM.Application.UseCases;
using System.Linq;

namespace ATM.UI.Pages;

public class EnterPinModel : PageModel
{
    private readonly EnterPinUseCase _useCase;

    [BindProperty]
    public string Pin { get; set; }

    public string? Message { get; private set; }

    public EnterPinModel(EnterPinUseCase useCase)
    {
        _useCase = useCase;
        Pin = "";
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(Pin))
        {
            Message = "Se requiere que ingrese un PIN.";
            return Page();
        }

        if(!(Pin.Length == 4 && Pin.All(char.IsDigit)))
        {
            Message = "El formato del pin es incorrecto. Ingrese solo 4 valores numéricos";
            return Page();
        }

        var isCorrect = await _useCase.ExecuteAsync(Pin);
        if (!isCorrect)
        {
            Message = "El pin ingresado es incorrecto.";
            return Page();
        }

        // TempData["CardId"] = CardId; // Save CardId for the next page
        return RedirectToPage("Menu");
    }
}