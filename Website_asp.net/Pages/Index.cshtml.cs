using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website_asp.net.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
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
