using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ATM.Application.Interfaces.Services;
using ATM.Application.Services;
using ATM.Application.UseCases;
using ATM.Application.Exceptions;
using System.Linq;

namespace ATM.UI.Pages;

public class EnterPinModel : PageModel
{
    private readonly EnterPinUseCase _useCase;

    [BindProperty]
    public string Pin { get; set; }

    public string? Message { get; private set; }

    public SessionService SessionService { get; set; }

    public EnterPinModel(EnterPinUseCase useCase, SessionService sessionService)
    {
        _useCase = useCase;
        Pin = "";
        SessionService = sessionService;
    }

    public IActionResult OnGet()
    {
        try
        {
            SessionService.GetCardId();   
            return Page();
        }
        catch (SessionException)
        {
            TempData["Message"] = "Por favor acerque su tarjeta para ingresar al sistema";
            return RedirectToPage("/Index");
        }
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