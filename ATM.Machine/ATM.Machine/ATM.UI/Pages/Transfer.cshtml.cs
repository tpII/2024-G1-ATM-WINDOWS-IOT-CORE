using ATM.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATM.UI.Pages;

public class TransferModel : PageModel
{
    private readonly Transfer _transfer;

    [BindProperty]
    public decimal Amount { get; set; }

    [BindProperty]
    public string DestinationCbu { get; set; }

    public string? ErrorMessage { get; set; }

    public TransferModel(Transfer transfer)
    {
        _transfer = transfer;
        DestinationCbu = "";
    }

    // Este método se ejecuta al enviar el formulario
    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(DestinationCbu))
        {
            ModelState.AddModelError("", "Debe ingresar un CBU");
            return Page();
        }

        if (Amount <= 0)
        {
            ModelState.AddModelError("", "El monto debe ser mayor a cero.");
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
            
            ErrorMessage = $"Hubo un error procesando su transferencia: {ex.Message}. \n Intentelo nuevamente";
        }

        return Page(); // Devuelve la misma página para mostrar el resultado
    }
}
