using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;
using VGProducts.Entities.Model;

namespace VGProducts.Entities.DTOs
{
    public class Order : BaseDataEntity
    {
        public required Guid OrderId { get; set; }
        public required string OrderNumber { get; set; }
        public required Guid UserId { get; set; }
        public OrderStatus Status { get; set; }
        public required decimal TotalAmount { get; set; }
        public required decimal ShippingAmount { get; set; }
        public required decimal FinalAmount { get; set; }
        public required Guid AddressId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus  PaymentStatus { get; set; }
        public string? Notes { get; set; }
        public required DateTime OrderdAt { get; set; }
        public DateTime? DeliveredAt { get; set; }


        // Navigation properties
        public ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Address Address { get; set; }
    }
}