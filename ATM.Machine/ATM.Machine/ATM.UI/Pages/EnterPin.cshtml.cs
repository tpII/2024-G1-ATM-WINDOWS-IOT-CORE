using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ATM.Application.Interfaces.Services;
using ATM.Application.Services;
using ATM.Application.UseCases;

namespace ATM.UI.Pages;

public class EnterPinModel : PageModel
{
    private readonly EnterPinUseCase _useCase;

    [BindProperty]
    public int Pin { get; set; }

    public string? Message { get; private set; }

    public EnterPinModel(EnterPinUseCase useCase)
    {
        _useCase = useCase;
        Pin = 0;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Pin == 0)
        {
            Message = "Se requiere que ingrese un PIN.";
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