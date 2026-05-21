using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGProducts.Business.Interface
{
    public interface IInvoiceBusiness
    {
        Task<byte[]> GenerateInvoiceAsync(Guid orderId, Guid userId);
    }
}
