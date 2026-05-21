using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class OrderDto : BaseDataEntity
    {
        public required Guid OrderId { get; set; }
        public required string OrderNumber { get; set; }
        public required Guid UserId { get; set; }
        public OrderStatus Status { get; set; }
        public required decimal TotalAmount { get; set; }
        public required decimal ShippingAmount { get; set; }
        public required decimal FinalAmount { get; set; }
        public required Guid ShippingAddressId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string? Notes { get; set; }
        public required DateTime OrderdAt { get; set; }
        public DateTime? DeliveredAt { get; set; }

    }
}
