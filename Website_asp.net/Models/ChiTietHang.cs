using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Website_asp.net.Models
{
    public class ChiTietHang
    {
            [Key]
            public int Id { get; set; }

            [Required]
            [Display(Name = "Tên sản phẩm")]
            public string ProductName { get; set; }

            [Required]
            [Display(Name = "Số lượng")]
            public int Quantity { get; set; }

            [Display(Name = "Mô tả")]
            public string? Description { get; set; }

            [ForeignKey("NhomHangId")]
            public int NhomHangId { get; set; }
            public NhomHang NhomHang { get; set; }
    }
}
