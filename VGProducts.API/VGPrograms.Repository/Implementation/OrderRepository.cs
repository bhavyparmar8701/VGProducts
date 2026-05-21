using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Entities.Model;
using VGProducts.Repository.DataAccess;
using VGProducts.Repository.Interface;

namespace VGProducts.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public OrderRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        private string GenerateOrderNumber()
        {
            return $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Random.Shared.Next(1000, 9999)}";
        }

        public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            //using var transaction = await applicationDbContext.Database.BeginTransactionAsync();
            var cart = await applicationDbContext.Cart.FirstOrDefaultAsync(c => c.UserId == createOrderDto.userId);
            if (cart == null)
                throw new Exception("Cart not found");
            var cartItems = await applicationDbContext.CartItems.Where(ci => ci.CartId == cart.CartId && ci.IsActive == IsActive.Active).ToListAsync();

            if (!cartItems.Any())
                throw new Exception("Cart is empty");

            var address = await applicationDbContext.Address.FirstOrDefaultAsync(a => a.AddressId == createOrderDto.AddressId && a.UserId == createOrderDto.userId);

            if (address == null)
                throw new Exception("Invalid address");

            var totalAmount = cartItems.Sum(c => c.SubTotal);
            var shippingAmount = totalAmount * 0.20m;
            var finalAmount = totalAmount + shippingAmount;

            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                OrderNumber = GenerateOrderNumber(),
                UserId = createOrderDto.userId,
                Status = OrderStatus.Pending,
                TotalAmount = totalAmount,
                ShippingAmount = shippingAmount,
                FinalAmount = finalAmount,
                AddressId = address.AddressId,
                OrderdAt = DateTime.UtcNow,
                DeliveredAt = DateTime.UtcNow.AddDays(5),
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            foreach (var item in cartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    SubTotal = item.SubTotal,
                    IsDeleted = false,
                });
            }
            await applicationDbContext.Order.AddAsync(order);
            applicationDbContext.CartItems.RemoveRange(cartItems);
            await applicationDbContext.SaveChangesAsync();
            //await transaction.CommitAsync();

            return new OrderResponseDto
            {
                OrderId = order.OrderId,
                OrderNumber = order.OrderNumber,
                ShippingAmount = order.ShippingAmount,
                FinalAmount = order.FinalAmount
            };
        }

        public async Task<List<OrderListDto>> GetOrdersAsync(Guid userId)
        {
            var orders = await applicationDbContext.Order.Where(o => o.UserId == userId).Include(o => o.OrderItems)
                .OrderByDescending(o => o.OrderdAt).ToArrayAsync();

            var result = orders.Select(o => new OrderListDto
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                OrderNumber = o.OrderNumber,
                Status = o.Status.ToString(),
                TotalAmount = o.TotalAmount,
                ShippingAmount = o.ShippingAmount,
                FinalAmount = o.FinalAmount,
                AddressId = o.AddressId,
                PaymentMethod = o.PaymentMethod.ToString(),
                PaymentStatus = o.PaymentStatus.ToString(),
                Notes = o.Notes,
                OrderdAt = o.OrderdAt,
                DeliveredAt = o.DeliveredAt,
                OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductName = oi.ProductName,
                    Price = oi.Price,
                    Quantity = oi.Quantity,
                    SubTotal = oi.SubTotal
                }).ToList()
            }).ToList();

            return result;
        }

        public string GenerateQrCode(string upiUrl)
        {
            var generator = new QRCoder.QRCodeGenerator();
            var qrCodeData = generator.CreateQrCode(upiUrl, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);

            byte[] qrbytes = qrCode.GetGraphic(20);

            return Convert.ToBase64String(qrbytes);
        }

        public async Task<string> DeleteOrderAsync(Guid orderId,Guid userId)
        {
            var order = applicationDbContext.Order.Include(o => o.OrderItems).FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return "Order not found";
            }
            if (order.Status != OrderStatus.Pending)
            {
                return "Only pending orders can be deleted";
            }
            order.IsDeleted = true;
            order.UpdatedAt = DateTime.UtcNow;
            order.Status = OrderStatus.Cancelled;

            foreach (var item in order.OrderItems)
            {
                item.IsDeleted = true;
                item.UpdatedAt = DateTime.UtcNow;
            }

            await applicationDbContext.SaveChangesAsync();
            return "Order deleted successfully";
        }

        public async Task<OrderPaymentResponseDto> SelectPaymentMethod(Guid orderId, PaymentMethod method, Guid userId)
        {
            var order = await applicationDbContext.Order.FirstOrDefaultAsync(o => o.OrderId == orderId && o.UserId == userId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            order.PaymentMethod = method;
            if (method == PaymentMethod.COD)
            {
                order.PaymentStatus = PaymentStatus.Paid;
                await applicationDbContext.SaveChangesAsync();
                return new OrderPaymentResponseDto
                {
                    OrderId = order.OrderId,
                    OrderNumber = order.OrderNumber,
                    FinalAmount = order.FinalAmount,
                    PaymentMethod = order.PaymentMethod.ToString(),
                    PaymentStatus = PaymentStatus.Paid.ToString(),
                    Message = "Payment successful with Cash on Delivery"
                };
            }
            order.PaymentStatus = PaymentStatus.Pendding;

            await applicationDbContext.SaveChangesAsync();
            return new OrderPaymentResponseDto
            {
                OrderId = order.OrderId,
                OrderNumber = order.OrderNumber,
                FinalAmount = order.FinalAmount,
                PaymentMethod = order.PaymentMethod.ToString(),
                PaymentStatus = PaymentStatus.Paid.ToString(),
                Message = "Scan the QR code to complete payment",
            };

        }

        public async Task<byte[]> GetPaymentQr(Guid userId)
        {
            var order = await applicationDbContext.Order.Where(o => o.UserId == userId && o.PaymentStatus == PaymentStatus.Pendding).OrderByDescending(o => o.CreatedAt).FirstOrDefaultAsync();
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            if (order.PaymentMethod != PaymentMethod.Online)
            {
                throw new Exception("Payment method is not UPI");
            }
            string upiUrl = $"upi://pay?pa=merchant@upi&pn=YourStore&am={order.FinalAmount}&cu=INR&tn=Order-{order.OrderNumber}";


            var generator = new QRCodeGenerator();
            var data = generator.CreateQrCode(upiUrl, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(data);

            byte[] qrBytes = qrCode.GetGraphic(10);

            

            order.PaymentStatus = PaymentStatus.Paid;
            return qrBytes;
        }
        
    }
}
