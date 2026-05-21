using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGProducts.Repository.Interface
{
    public interface IInvoiceRepository
    {
        Task<byte[]> GenerateInvoiceAsync(Guid orderId, Guid userId);
    }
}
