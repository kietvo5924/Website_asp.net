using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website_asp.net.Pages
{
    public class ThemNhomHangModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ThemNhomHangModel> _logger;

        public ThemNhomHangModel(AppDbContext context, ILogger<ThemNhomHangModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet(int Id)
        {
            // Lấy danh sách địa chỉ từ cơ sở dữ liệu và gán cho ListDiaChi
            ListDiaChi = _context.DiaChis.ToList();

            // Kiểm tra xem Session UserName đã tồn tại hay không
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                // Nếu không, chuyển hướng người dùng đến trang đăng nhập
                return RedirectToPage("/Privacy");
            }

            return Page();
        }

        public DateTime CreatedAt { get; set; }
        public List<DiaChi> ListDiaChi { get; set; }

        public IActionResult OnPostLogout()
        {
            // Xóa Session UserName để đăng xuất người dùng
            HttpContext.Session.Remove("UserName");

            // Trả về mã trạng thái HTTP 200 OK
            return RedirectToPage("/Privacy");
        }

        public IActionResult OnPostLuuThongTinNhomHang(NhomHang nhomHang)
        {
            if (ModelState.IsValid)
            {
              // nhomHang.CreatedAt = DateTime.Now;

                // Lưu thông tin nhóm hàng vào cơ sở dữ liệu
                _context.NhomHangs.Add(nhomHang);
                _context.SaveChanges();

                // Chuyển hướng hoặc thực hiện các thao tác khác sau khi lưu
                return RedirectToPage("NhomHang");
            }

            var existingNhomHang = _context.NhomHangs.FirstOrDefault(n => n.Id == nhomHang.Id);

            if (existingNhomHang != null)
            {
                // Xử lý khi đối tượng đã tồn tại (trùng lặp)
                // Ví dụ: Hiển thị thông báo lỗi, yêu cầu nhập lại, v.v.
                ModelState.AddModelError("Id", "Id đã tồn tại. Vui lòng nhập lại.");
                return Page();
            }
            else
            {
                _context.NhomHangs.Add(nhomHang);
                _context.SaveChanges();

                // Chuyển hướng sau khi lưu thành công
                return RedirectToPage("NhomHang");
            }
        }
    }
}
