using System.ComponentModel.DataAnnotations;

namespace Website_asp.net.Models
{
    public class DiaChi
    {
        [Key]
        public int MaTT { get; set; }

        [Required]
        public string TenTinh { get; set; }
    }
}
