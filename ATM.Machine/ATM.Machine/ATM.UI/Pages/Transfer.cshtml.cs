
using ATM.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ATM.Application.Services;

namespace ATM.UI.Pages;

public class TransferModel : PageModel
{
    [BindProperty]
    public decimal Amount { get; set; }

    [BindProperty]
    public string DestinationCbu { get; set; }

    public string? ErrorMessage { get; set; }

    public SessionService SessionService { get; set; }
    
    private readonly Transfer _transfer;

    public TransferModel(Transfer transfer, SessionService sessionService)
    {
        _transfer = transfer;
        DestinationCbu = "";
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
        if (string.IsNullOrEmpty(DestinationCbu))
        {
            ErrorMessage = "Debe ingresar un CBU";
            return Page();
        }

        if (Amount <= 0)
        {
            ErrorMessage = "El monto debe ser mayor a cero.";
            return Page();
        }

        try
        {
            await _transfer.ExecuteAsync(DestinationCbu, Amount);
            TempData["Message"] = "Transferencia realizada con éxito.";
            return RedirectToPage("/Menu");
        }
        catch (Exception ex)
        {
            
            ErrorMessage = ex.Message;
        }

        return Page(); // Devuelve la misma página para mostrar el resultado
    }
}
