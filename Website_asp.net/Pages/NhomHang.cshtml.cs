using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Dynamic;

namespace Website_asp.net.Pages
{
    public class NhomHangModel : PageModel
    {

        private readonly ILogger<NhomHangModel> _logger;
        private readonly AppDbContext _context;

        public NhomHangModel(AppDbContext context, ILogger<NhomHangModel> logger)
        {
            _logger = logger;
            _context = context;
        }

        public List<NhomHang> NhomHangList { get; set; }

        public IActionResult OnPostLogout()
        {
            // Xóa Session UserName ?? ??ng xu?t ng??i dùng
            HttpContext.Session.Remove("UserName");

            // Tr? v? mã tr?ng thái HTTP 200 OK
            return RedirectToPage("/Privacy");
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Ki?m tra xem Session UserName ?ã t?n t?i hay không
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                // N?u không, chuy?n h??ng ng??i dùng ??n trang ??ng nh?p
                return RedirectToPage("/Privacy");
            }

            NhomHangList = await _context.NhomHangs.ToListAsync();
            foreach (var nhomHang in NhomHangList)
            {
                nhomHang.DiaChi = await _context.DiaChis.FindAsync(nhomHang.DiaChiId);
            }

            return Page();
        }

        public async Task<IActionResult> OnGetDeleteAsync(int id)
        {
            var nhomHang = await _context.NhomHangs.FindAsync(id);

            if (nhomHang == null)
            {
                return NotFound();
            }

            _context.NhomHangs.Remove(nhomHang);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
