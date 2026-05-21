using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class AddCartItemDto
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
