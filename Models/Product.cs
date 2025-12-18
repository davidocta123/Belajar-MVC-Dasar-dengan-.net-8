using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ECommerceApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama produk wajib diisi.")]
        [StringLength(100, ErrorMessage = "Nama produk maksimal 100 karakter")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Harga wajib diisi")]
        [Range(0, double.MaxValue, ErrorMessage = "Harga harus lebih besar atau sama dengan 0")]
        public decimal Price { get; set; }

        [StringLength(255, ErrorMessage = "URL gambar maksimal 255 karakter")]
        public string? ImageUrl { get; set; }

        [StringLength(500, ErrorMessage = "Deskripsi maksimal 500 karakter")]
        public string? Description { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
