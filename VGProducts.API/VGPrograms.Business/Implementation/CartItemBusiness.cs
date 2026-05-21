using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Repository.Interface;

namespace VGProducts.Business.Implementation
{
    public class CartItemBusiness : ICartItemBusiness
    {
        private readonly ICartItemRepository cartItemRepository;

        public CartItemBusiness(ICartItemRepository cartItemRepository)
        {
            this.cartItemRepository = cartItemRepository;
        }

        public async Task<string> AddByIdCartItemAsync(Guid id, Guid userId)
        {
            return await cartItemRepository.AddByIdCartItemAsync(id, userId);
        }

        public async Task<CartResponseDto> AddToCart( AddCartItemDto addCartItemDto)
        {
            return await cartItemRepository.AddToCart(addCartItemDto);
        }

        public async Task<string> DeleteAllCartItemAsync(Guid userId)
        {
            return await cartItemRepository.DeleteAllCartItemAsync(userId);
        }

        public async Task<string> DeleteByIdCartItemAsync(Guid Id, Guid userId)
        {
            return await cartItemRepository.DeleteByIdCartItemAsync(Id, userId);
        }

        public async Task<CartWithItemsDto> GetAllCartItem(Guid userId)
        {
            return await cartItemRepository.GetAllCartItem(userId);
        }
    }
}
