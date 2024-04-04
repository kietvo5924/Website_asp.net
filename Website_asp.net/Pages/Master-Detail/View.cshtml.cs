using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website_asp.net.Pages.Master_Detail
{
    public class ViewModel : PageModel
    {
        private readonly ILogger<ViewModel> _logger;
        private readonly AppDbContext _context;

        public ViewModel(AppDbContext context, ILogger<ViewModel> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult OnGet(int Id)
        {
            // Ki?m tra xem Session UserName ?� t?n t?i hay kh�ng
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                // N?u kh�ng, chuy?n h??ng ng??i d�ng ??n trang ??ng nh?p
                return RedirectToPage("/Privacy");
            }

            return Page();
        }

        public IActionResult OnPostLogout()
        {
            // X�a Session UserName ?? ??ng xu?t ng??i d�ng
            HttpContext.Session.Remove("UserName");

            // Tr? v? m� tr?ng th�i HTTP 200 OK
            return RedirectToPage("/Privacy");
        }
    }
}
