using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using ATM.Core.UseCases;

namespace ATM.UI.Pages
{
    public class DepositModel : PageModel
    {
        public string Message { get; set; }

        private readonly DepositUseCase _depositUseCase;

        public DepositModel (DepositUseCase depositUseCase)
        {
            _depositUseCase = depositUseCase;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(decimal amount)
        {
            var result = await _depositUseCase.Execute(amount);   
            string st = result ? "Exitoso" : "Erroneo";
            Message = $"Resultado de la operación: {st}";
            Console.WriteLine(Message);

            return Page();
        }
    }
}