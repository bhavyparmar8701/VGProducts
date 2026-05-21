using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;

namespace VGProducts.Business.Interface
{
    public interface IOrderBusiness
    {
        Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto dto);
        Task<List<OrderListDto>> GetOrdersAsync(Guid userId);
        Task<string> DeleteOrderAsync(Guid orderId, Guid userId);
        Task<OrderPaymentResponseDto> SelectPaymentMethod(Guid orderId, PaymentMethod method, Guid userId);
        Task<byte[]> GetPaymentQr(Guid orderId);
    }
}
