using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGProducts.Entities.Enums
{
    public enum OrderStatus
    {
        Pending = 1,
        Processing = 2,
        Shipping = 3,
        Delivered = 4,
        Cancelled = 5
    }
}
