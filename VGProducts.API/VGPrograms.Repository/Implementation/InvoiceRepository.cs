using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.EntityFrameworkCore;
using VGProducts.Repository.DataAccess;
using VGProducts.Repository.Interface;

namespace VGProducts.Repository.Implementation
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public InvoiceRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<byte[]> GenerateInvoiceAsync(Guid orderId, Guid userId)
        {
            var order = await applicationDbContext.Order
                                .Include(o => o.OrderItems)
                                .Include(o => o.ApplicationUser)   // ✅ ADD THIS
                                .Include(o => o.Address)
                                    .ThenInclude(a => a.City)
                                .Include(o => o.Address)
                                    .ThenInclude(a => a.State)
                                .Include(o => o.Address)
                                    .ThenInclude(a => a.Country)
                                .FirstOrDefaultAsync(o => o.OrderId == orderId
                                    && o.UserId == userId
                                    && o.IsDeleted == false);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            using var stream = new MemoryStream();

            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            // 🔷 Title
            document.Add(new Paragraph("VGProducts Invoice")
                .SetFontSize(20)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

            // 🔷 Order Info
            document.Add(new Paragraph($"Order Number: {order.OrderNumber}"));
            document.Add(new Paragraph($"Order Date: {order.CreatedAt:dd-MM-yyyy HH:mm:ss}"));
            document.Add(new Paragraph("\n"));

            // 🔷 Customer Details
            document.Add(new Paragraph("Customer Details"));

            document.Add(new Paragraph($"Name: {order.ApplicationUser.FirstName} {order.ApplicationUser.LastName}"));
            document.Add(new Paragraph($"Email: {order.ApplicationUser.Email}"));
            document.Add(new Paragraph($"Phone: {order.ApplicationUser.PhoneNumber}"));
            document.Add(new Paragraph($"Address: {order.Address.LandMark}, {order.Address.AddressLine1}, {order.Address.AddressLine2}, {order.Address.City.CityName}, {order.Address.State.StateName}, {order.Address.Country.CountryName}"));
            document.Add(new Paragraph("\n"));

            // 🔷 Payment Details
            document.Add(new Paragraph("Payment Details"));

            document.Add(new Paragraph($"Payment Method: {order.PaymentMethod.ToString()}"));
            document.Add(new Paragraph($"Payment Status: {order.PaymentStatus.ToString()}"));
            document.Add(new Paragraph("\n"));

            // 🔷 Table (100% width)
            var table = new Table(new float[] { 4, 2, 2, 2 })
                .UseAllAvailableWidth();

            // Header
            table.AddHeaderCell("Product Name");
            table.AddHeaderCell("Price");
            table.AddHeaderCell("Quantity");
            table.AddHeaderCell("Subtotal");

            // Data
            foreach (var item in order.OrderItems)
            {
                table.AddCell(item.ProductName);
                table.AddCell(item.Price.ToString("0.00"));
                table.AddCell(item.Quantity.ToString());
                table.AddCell(item.SubTotal.ToString("0.00"));
            }

            document.Add(table);
            document.Add(new Paragraph("\n"));

            // 🔷 Summary
            document.Add(new Paragraph($"Total Amount: {order.TotalAmount:0.00}").SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));
            document.Add(new Paragraph($"Shipping Amount: {order.ShippingAmount:0.00}").SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));
            document.Add(new Paragraph($"Grand Total: {order.FinalAmount:0.00}").SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));

            document.Add(new Paragraph("\nThank you for shopping with VGProducts!")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

            document.Close();
            return stream.ToArray();
        }
    }
}
