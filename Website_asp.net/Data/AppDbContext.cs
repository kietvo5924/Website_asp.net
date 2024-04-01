using System.ComponentModel.DataAnnotations.Schema;
using Website_asp.net.Models;
using Microsoft.EntityFrameworkCore;

namespace Website_asp.net.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<DiaChi> DiaChis { get; set; }
        public DbSet<NhomHang> NhomHangs { get; set; }
    }
}
