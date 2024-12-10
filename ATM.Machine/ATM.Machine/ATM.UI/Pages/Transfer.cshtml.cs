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

    public string? SuccessMessage { get; set; }
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
            ErrorMessage = "Se requiere que ingrese un CBU destino.";
            return Page();
        }

        try
        {
            await _transfer.ExecuteAsync(DestinationCbu, Amount);
            SuccessMessage = $"Successfully transfered {Amount:C} to account with CBU:{DestinationCbu}.";
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }

        return Page(); // Devuelve la misma página para mostrar el resultado
    }
}
