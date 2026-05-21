using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;

namespace VGProducts.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto dto);
        Task<List<OrderListDto>> GetOrdersAsync(Guid userId);
        Task<string> DeleteOrderAsync(Guid orderId, Guid userId);
        Task<OrderPaymentResponseDto> SelectPaymentMethod(Guid orderId, PaymentMethod method, Guid userId);
        Task<byte[]> GetPaymentQr(Guid orderId);
    }
}
