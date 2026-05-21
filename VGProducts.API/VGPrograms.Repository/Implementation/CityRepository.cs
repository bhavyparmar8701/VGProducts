using Microsoft.EntityFrameworkCore;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Entities.Model;
using VGProducts.Repository.DataAccess;
using VGProducts.Repository.Interface;

namespace VGProducts.Repository.Implementation
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CityRepository(ApplicationDbContext applicationDbContext) 
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<AddCityDto> AddCityAsync(AddCityDto addCityDto)
        {
            var data = new City
            {
                CityName = addCityDto.CityName,
                StateId = addCityDto.StateId,
                IsActive = IsActive.Active,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
            };
            await applicationDbContext.City.AddAsync(data);
            await applicationDbContext.SaveChangesAsync();
            return new AddCityDto
            {
                CityName = data.CityName,
                StateId = data.StateId
            };
        }

        public async Task<string> DeleteCityAsync(Guid id)
        {
            var city = await applicationDbContext.City.FirstOrDefaultAsync(c => c.CityId == id);
            if (city == null)
            {
                return "City Not Found";
            }
            city.IsActive = IsActive.Inactive;
            city.UpdatedAt = DateTime.UtcNow;
            city.IsDeleted = true;

            await applicationDbContext.SaveChangesAsync();
            return "City deleted successfully";
        }

        public async Task<List<CityDto>> GetAllCityAsync()
        {
            var city = await applicationDbContext.City.ToListAsync();
            return city.Select(c => new CityDto
            {
                CityId = c.CityId,
                CityName = c.CityName,
                StateId = c.StateId,
                IsActive = c.IsActive.ToString(),
                CreatedAt = c.CreatedAt
            }).ToList();
        }

        public async Task<CityDto> GetCityByIdAsync(Guid Stateid)
        {
            var city = await applicationDbContext.City.Where(c => c.StateId == Stateid).FirstOrDefaultAsync();
            return new CityDto  
            {
                CityId = city.CityId,
                CityName = city.CityName,
                StateId = city.StateId,
                IsActive = city.IsActive.ToString(),
                CreatedAt = city.CreatedAt
            };
        }
    }
}
