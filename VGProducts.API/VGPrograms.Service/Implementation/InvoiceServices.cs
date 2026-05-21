using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Business.Interface;
using VGProducts.Service.Interface;

namespace VGProducts.Service.Implementation
{
    public class InvoiceServices : IInvoiceServices
    {
        private readonly IInvoiceBusiness invoiceBusiness;

        public InvoiceServices(IInvoiceBusiness invoiceBusiness)
        {
            this.invoiceBusiness = invoiceBusiness;
        }

        public async Task<byte[]> GenerateInvoiceAsync(Guid orderId, Guid userId)
        {
            return await invoiceBusiness.GenerateInvoiceAsync(orderId, userId);
        }
    }
}
