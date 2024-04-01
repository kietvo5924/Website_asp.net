using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Website_asp.net.Models;

namespace Website_asp.net.Pages
{
    public class EditNhomHangModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EditNhomHangModel> _logger;

        public EditNhomHangModel(AppDbContext context, ILogger<EditNhomHangModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public class EditNhomHangViewModel
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Tên nhóm là bắt buộc")]
            [Display(Name = "Tên Nhóm")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Giá là bắt buộc")]
            [Display(Name = "Giá")]
            public decimal Price { get; set; }

            [Display(Name = "Mô tả")]
            public string Description { get; set; }

            [Display(Name = "Thời điểm tạo ra")]
            public DateTime CreatedAt { get; set; }

            [Display(Name = "Địa chỉ ID")]
            public int DiaChiId { get; set; } // Thêm thuộc tính DiaChiId vào ViewModel
        }

        [BindProperty]
        public EditNhomHangViewModel NhomHangToUpdate { get; set; }
        public List<DiaChi> ListDiaChi { get; private set; }

        public IActionResult OnPostLogout()
        {
            // Xóa Session UserName để đăng xuất người dùng
            HttpContext.Session.Remove("UserName");

            // Trả về mã trạng thái HTTP 200 OK
            return RedirectToPage("/Privacy");
        }

        public async Task<IActionResult> OnGetAsync(int Id)
        {
            // Lấy danh sách các địa chỉ từ cơ sở dữ liệu
            ListDiaChi = await _context.DiaChis.ToListAsync();

            NhomHang nhomHang = _context.NhomHangs.Find(Id);
            if (nhomHang == null)
            {
                return NotFound();
            }

            NhomHangToUpdate = new EditNhomHangViewModel
            {
                Id = nhomHang.Id,
                Name = nhomHang.Name,
                Price = nhomHang.Price,
                Description = nhomHang.Description,
                CreatedAt = nhomHang.CreatedAt,
                DiaChiId = nhomHang.DiaChiId
            };

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            NhomHang nhomHang = _context.NhomHangs.Find(NhomHangToUpdate.Id);
            if (nhomHang == null)
            {
                return NotFound();
            }

            nhomHang.Name = NhomHangToUpdate.Name;
            nhomHang.Price = NhomHangToUpdate.Price;
            nhomHang.Description = NhomHangToUpdate.Description;
            nhomHang.DiaChiId = NhomHangToUpdate.DiaChiId;

            try
            {
                _context.Update(nhomHang);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhomHangExists(NhomHangToUpdate.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/NhomHang");
        }

        private bool NhomHangExists(int Id)
        {
            return _context.NhomHangs.Any(e => e.Id == Id);
        }
    }
}
