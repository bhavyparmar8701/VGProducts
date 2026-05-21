using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;

namespace VGProducts.Repository.Interface
{
    public interface ICartItemRepository
    {
        Task<CartResponseDto> AddToCart( AddCartItemDto addCartItemDto);
        Task<CartWithItemsDto> GetAllCartItem(Guid userId);
        Task<string> DeleteByIdCartItemAsync(Guid Id, Guid userId);
        Task<string> DeleteAllCartItemAsync(Guid userId);
        Task<string> AddByIdCartItemAsync(Guid id, Guid userId);
    }
}
