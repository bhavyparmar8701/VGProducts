using VGProducts.Entities.Base;

namespace VGProducts.Entities.DTOs
{
    public class OrderItem : BaseDataEntity
    {
        public Guid ID { get; set; }

        public required Guid OrderId { get; set; }

        public required Guid ProductId { get; set; }
        public required string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }

        // Navigation properties
        public  Order Order { get; set; }
        public  Product Product { get; set; }
 
    }
}