using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class CreateOrderDto
    {
        public Guid userId { get; set; }
        public Guid AddressId { get; set; }
    }
}
