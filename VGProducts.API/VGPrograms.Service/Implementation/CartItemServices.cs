using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.Service.Implementation
{
    public class CartItemServices : ICartItemServices
    {
        private readonly ICartItemBusiness cartItemBusiness;

        public CartItemServices(ICartItemBusiness cartItemBusiness)
        {
            this.cartItemBusiness = cartItemBusiness;
        }

        public async Task<string> AddByIdCartItemAsync(Guid id, Guid userId)
        {
            return await cartItemBusiness.AddByIdCartItemAsync(id, userId);
        }

        public async Task<CartResponseDto> AddToCart( AddCartItemDto addCartItemDto)
        {
            return await cartItemBusiness.AddToCart( addCartItemDto);
        }

        public async Task<string> DeleteAllCartItemAsync(Guid userId)
        {
            return await cartItemBusiness.DeleteAllCartItemAsync(userId);
        }

        public async Task<string> DeleteByIdCartItemAsync(Guid Id, Guid userId)
        {
            return await cartItemBusiness.DeleteByIdCartItemAsync(Id, userId);
        }

        public async Task<CartWithItemsDto> GetAllCartItem(Guid userId)
        {
            return await cartItemBusiness.GetAllCartItem(userId);
        }
    }
}
