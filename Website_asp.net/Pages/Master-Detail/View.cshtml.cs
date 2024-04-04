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
            // Ki?m tra xem Session UserName ?ã t?n t?i hay không
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                // N?u không, chuy?n h??ng ng??i dùng ??n trang ??ng nh?p
                return RedirectToPage("/Privacy");
            }

            return Page();
        }

        public IActionResult OnPostLogout()
        {
            // Xóa Session UserName ?? ??ng xu?t ng??i dùng
            HttpContext.Session.Remove("UserName");

            // Tr? v? mã tr?ng thái HTTP 200 OK
            return RedirectToPage("/Privacy");
        }
    }
}
