using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Repository.Interface;

namespace VGProducts.Business.Implementation
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IOrderRepository orderRepository;

        public OrderBusiness(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        

        public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto dto)
        {
            return await orderRepository.CreateOrderAsync(dto);
        }

        public async Task<string> DeleteOrderAsync(Guid orderId, Guid userId)
        {
            return await orderRepository.DeleteOrderAsync(orderId, userId);
        }

        public async Task<List<OrderListDto>> GetOrdersAsync(Guid userId)
        {
            return await orderRepository.GetOrdersAsync(userId);
        }

        public async Task<byte[]> GetPaymentQr(Guid orderId)
        {
            return await orderRepository.GetPaymentQr(orderId);
        }

        public async Task<OrderPaymentResponseDto> SelectPaymentMethod(Guid orderId, PaymentMethod method, Guid userId)
        {
            return await orderRepository.SelectPaymentMethod(orderId, method, userId);
        }
    }
}
