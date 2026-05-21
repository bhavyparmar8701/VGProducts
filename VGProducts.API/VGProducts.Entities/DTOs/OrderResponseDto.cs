using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class OrderResponseDto
    {
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal FinalAmount { get; set; }
        
    }
}
