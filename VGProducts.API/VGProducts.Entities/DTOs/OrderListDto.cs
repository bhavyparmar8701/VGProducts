using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class OrderListDto
    {
        public Guid OrderId { get; set; }
        public required Guid UserId { get; set; }
        public string OrderNumber { get; set; }
        public string Status { get; set; }
        public required decimal TotalAmount { get; set; }
        public required decimal ShippingAmount { get; set; }
        public required decimal FinalAmount { get; set; }
        public required Guid AddressId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string? Notes { get; set; }
        public required DateTime OrderdAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
