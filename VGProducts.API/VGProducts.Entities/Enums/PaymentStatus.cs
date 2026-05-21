using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGProducts.Entities.Enums
{
    public enum PaymentStatus
    {
        Pendding = 1,
        Paid = 2,
        Failed = 3,
        Refunded = 4,
        Unpaid = 5
    }
}
