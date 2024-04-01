using System.ComponentModel.DataAnnotations;

namespace Website_asp.net.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
