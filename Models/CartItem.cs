using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; } // Primary Key unik

        public int ProductId { get; set; }

        public string ProductName { get; set; } = "";
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }

        public int UserId { get; set; } // Menyimpan siapa pemilik cart

        public decimal SubTotal => Price * Quantity;
    }
}
