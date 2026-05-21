using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;

namespace VGProducts.Repository.Interface
{
    public interface IFavouritesRepository
    {
        Task<AddFavouritesDto> AddFavouritesAsync(AddFavouritesDto addFavouritesDto);
        Task<FavouritesWithUserDto> GetAllFavouritesAsync(Guid userId);

        Task<string> DeleteFavouritesAsync(Guid productId, Guid userId);
    }
}
