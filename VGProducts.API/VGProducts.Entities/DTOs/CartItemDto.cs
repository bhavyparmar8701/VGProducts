using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class CartItemDto 
    {
        public Guid Id { get; set; }
        public required string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal {  get; set; }
        public IsActive IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
