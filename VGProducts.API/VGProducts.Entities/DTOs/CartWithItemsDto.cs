using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGProducts.Entities.DTOs
{
    public class CartWithItemsDto
    {
        public Guid UserId { get; set; }
        public Guid CartId { get; set; }
        public List<CartItemDto> Items { get; set; }
    }
}
