using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Service.Interface;

namespace VGProducts.Service.Implementation
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderBusiness orderBusiness;

        public OrderServices(IOrderBusiness orderBusiness)
        {
            this.orderBusiness = orderBusiness;
        }

        

        public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto dto)
        {
            return await orderBusiness.CreateOrderAsync(dto);
        }

        public async Task<string> DeleteOrderAsync(Guid orderId, Guid userId)
        {
            return await orderBusiness.DeleteOrderAsync(orderId, userId);
        }

        public async Task<List<OrderListDto>> GetOrdersAsync(Guid userId)
        {
            return await orderBusiness.GetOrdersAsync(userId);
        }

        public async Task<byte[]> GetPaymentQr(Guid orderId)
        {
            return await orderBusiness.GetPaymentQr(orderId);
        }

        public async Task<OrderPaymentResponseDto> SelectPaymentMethod(Guid orderId, PaymentMethod method, Guid userId)
        {
            return await orderBusiness.SelectPaymentMethod(orderId, method, userId);
        }
    }
}
