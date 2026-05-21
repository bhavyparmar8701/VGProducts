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
    public class FavouritesServices : IFavouritesServices
    {
        private readonly IFavouritesBusiness favouritesBusiness;

        public FavouritesServices(IFavouritesBusiness favouritesBusiness)
        {
            this.favouritesBusiness = favouritesBusiness;
        }
        public async Task<AddFavouritesDto> AddFavouritesAsync( AddFavouritesDto addFavouritesDto)
        {
           return await favouritesBusiness.AddFavouritesAsync(addFavouritesDto);
        }

        public async Task<string> DeleteFavouritesAsync(Guid productId, Guid userId)
        {
            return await favouritesBusiness.DeleteFavouritesAsync(productId, userId);
        }

        public async Task<FavouritesWithUserDto> GetAllFavouritesAsync(Guid userId)
        {
            return await favouritesBusiness.GetAllFavouritesAsync(userId);
        }
    }
}
