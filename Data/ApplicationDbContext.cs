using Microsoft.EntityFrameworkCore;
using ECommerceApp.Models;
namespace ECommerceApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } // Merepresentasikan tabel Products
                                                     // Anda bisa menambahkan konfigurasi model tambahan di sini jika diperlukan
        public DbSet<CartItem> CartItems { get; set; } // Merepresentasikan tabel CartItems

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Contoh: Mengatur nama tabel secara eksplisit
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<CartItem>().ToTable("CartItems");

        }
    }
}