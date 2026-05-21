using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGProducts.Service.Interface
{
    public interface IInvoiceServices
    {
        Task<byte[]> GenerateInvoiceAsync(Guid orderId, Guid userId);
    }
}
