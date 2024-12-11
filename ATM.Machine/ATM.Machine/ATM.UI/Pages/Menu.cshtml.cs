using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ATM.Application.Services;

namespace ATM.UI.Pages
{
    public class MenuModel : PageModel
    {

        private readonly SessionService _sessionService;

        public MenuModel(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public IActionResult OnPostLogOut()
        {
            // // Lógica para cerrar sesión (ej. eliminar cookies, sesión, etc.)
            // HttpContext.Session.Clear(); // Limpia la sesión, si aplica.
            _sessionService.LogOut();
            return RedirectToPage("/Index"); // Redirige al Index.
        }
    }
}
