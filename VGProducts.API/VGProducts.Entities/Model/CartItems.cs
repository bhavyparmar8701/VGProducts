using VGProducts.Entities.Base;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.Model
{

    public class CartItems : BaseDataEntity
    {
        public Guid Id { get; set; }
        public required Guid CartId { get; set; }
        public required Guid ProductId { get; set; }
        public required string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public IsActive IsActive { get; set; }

        // Navigation properties
        public Product Product { get; set; }
        public Cart Cart { get; set; }

    }
}
