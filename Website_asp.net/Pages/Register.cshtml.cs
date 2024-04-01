using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace Website_asp.net.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;
        private readonly AppDbContext _context;

        public RegisterModel(AppDbContext context, ILogger<RegisterModel> logger)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public IActionResult OnPostLogout()
        {
            // X�a Session UserName ?? ??ng xu?t ng??i d�ng
            HttpContext.Session.Remove("UserName");

            // Tr? v? m� tr?ng th�i HTTP 200 OK
            return RedirectToPage("/Privacy");
        }

        public IActionResult OnPostRegister()
        {
            if (string.IsNullOrEmpty(Customer.Password))
            {
                ModelState.AddModelError(string.Empty, "Password is required.");
                return Page();
            }
            if (!IsValidPassword(Customer.Password))
            {
                ModelState.AddModelError(string.Empty, "Password must be at least 8 characters long and contain at least one special character.");
                return Page();
            }

            if (ModelState.IsValid)
            {
                // B?m m?t kh?u tr??c khi l?u v�o c? s? d? li?u
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Customer.Password);

                // Th�m th�ng tin ng??i d�ng v�o c? s? d? li?u
                Customer.Password = hashedPassword;

                // Th�m th�ng tin ng??i d�ng v�o c? s? d? li?u
                _context.Customers.Add(Customer);
                _context.SaveChanges();

                // Chuy?n h??ng sau khi ??ng k� th�nh c�ng
                return RedirectToPage("/Privacy", new { UserName = Customer.UserName });
            }

            // X? l� khi ModelState kh�ng h?p l?
            return Page();
        }

        private bool IsValidPassword(string password)
        {
            // Ki?m tra xem m?t kh?u c� �t nh?t 8 k� t? v� c� �t nh?t m?t k� t? ??c bi?t
            return Regex.IsMatch(password, @"(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?/~`])\S{8,}");
        }
    }
}
