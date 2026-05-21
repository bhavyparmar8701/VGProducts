using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Entities.Model;
using VGProducts.Repository.DataAccess;
using VGProducts.Repository.Interface;

namespace VGProducts.Repository.Implementation
{
    public class FavouritesRepository : IFavouritesRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public FavouritesRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }


        public async Task<AddFavouritesDto> AddFavouritesAsync(AddFavouritesDto addFavouritesDto)
        {
            var user = await applicationDbContext.Users.AnyAsync(f => f.Id == addFavouritesDto.UserId && f.IsDeleted == false);
            if (user == null)
            {
                return null;
            }

            var product = await applicationDbContext.Product.AnyAsync(p => p.ProductId == addFavouritesDto.ProductId && p.IsActive == IsActive.Active && p.IsDeleted == false);
            if(product == null)
            {
                return null;
            }

            var existingFavourite = await applicationDbContext.Favourites.FirstOrDefaultAsync(f => f.UserId == addFavouritesDto.UserId && f.ProductId == addFavouritesDto.ProductId && f.IsDeleted == false);
            if (existingFavourite != null)
            {
                throw new Exception("Product already added to favourites");
            }

            var data = new Favourites
            {
                UserId = addFavouritesDto.UserId,
                ProductId = addFavouritesDto.ProductId,
                IsActive = IsActive.Active,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await applicationDbContext.Favourites.AddAsync(data);
            await applicationDbContext.SaveChangesAsync();

            return new AddFavouritesDto
            {
                UserId = data.UserId,  
                ProductId = data.ProductId
            };
        }

        public async Task<string> DeleteFavouritesAsync(Guid productId,Guid userId)
        {

            var favourites = await applicationDbContext.Favourites.FirstOrDefaultAsync(f => f.ProductId == productId && f.UserId == userId && f.IsDeleted == false);
            if (favourites == null)
            {
                return "Favourites Not Found";
            }
            else
            {
                favourites.IsActive = IsActive.Inactive;
                favourites.UpdatedAt = DateTime.UtcNow;
                favourites.IsDeleted = true;

                await applicationDbContext.SaveChangesAsync();
                return "Favourites Deletes Successfully";
            }
            
            throw new NotImplementedException();
        }

        public async Task<FavouritesWithUserDto> GetAllFavouritesAsync(Guid userId)
        {
            var favourites = await applicationDbContext.Favourites.Where(f => f.UserId == userId && f.IsActive == IsActive.Active).ToListAsync();
            return new FavouritesWithUserDto
            {
                UserId = userId,

                Favourites = favourites.Select(f => new FavouritesDto
                {
                    FavouritesId = f.FavouritesId,
                    UserId = f.UserId,
                    ProductId = f.ProductId,
                    IsActive = f.IsActive.ToString(),
                    CreatedAt = f.CreatedAt,
                }).ToList()
            };
        }
    }
}
