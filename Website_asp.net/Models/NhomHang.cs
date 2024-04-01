using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Website_asp.net.Models
{
    public class NhomHang
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tên Nhóm")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Giá")]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Thời điểm tạo ra")]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("DiaChiId")]
        public int DiaChiId { get; set; }
        public DiaChi DiaChi { get; set; }

    /*  public NhomHang()
        {
            CreatedAt = DateTime.Now;
        } 
    */
    }
}
