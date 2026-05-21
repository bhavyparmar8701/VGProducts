using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Business.Interface;
using VGProducts.Repository.Interface;

namespace VGProducts.Business.Implementation
{
    public class InvoiceBusiness : IInvoiceBusiness
    {
        private readonly IInvoiceRepository invoiceRepository;

        public InvoiceBusiness(IInvoiceRepository invoiceRepository) 
        {
            this.invoiceRepository = invoiceRepository;
        }

        public async Task<byte[]> GenerateInvoiceAsync(Guid orderId, Guid userId)
        {
            return await invoiceRepository.GenerateInvoiceAsync(orderId, userId);
        }
    }
}
