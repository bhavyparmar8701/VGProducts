using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class OrderPaymentResponseDto
    {
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; }
        public decimal FinalAmount { get; set; }
        public string Message { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
    }
}
