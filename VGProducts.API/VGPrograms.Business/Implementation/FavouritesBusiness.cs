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
    public class FavouritesBusiness : IFavouritesBusiness
    {
        private readonly IFavouritesRepository favouritesRepository;

        public FavouritesBusiness(IFavouritesRepository favouritesRepository)
        {
            this.favouritesRepository = favouritesRepository;
        }
        public async Task<AddFavouritesDto> AddFavouritesAsync(AddFavouritesDto addFavouritesDto)
        {
            return await favouritesRepository.AddFavouritesAsync(addFavouritesDto);
        }

        public async Task<string> DeleteFavouritesAsync(Guid productId, Guid userId)
        {
            return await favouritesRepository.DeleteFavouritesAsync(productId, userId);
        }

        public async Task<FavouritesWithUserDto> GetAllFavouritesAsync(Guid userId)
        {
            return await favouritesRepository.GetAllFavouritesAsync(userId);
        }
    }
}
