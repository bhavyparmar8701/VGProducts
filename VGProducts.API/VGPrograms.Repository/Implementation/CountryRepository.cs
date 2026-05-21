using Microsoft.EntityFrameworkCore;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Entities.Model;
using VGProducts.Repository.DataAccess;
using VGProducts.Repository.Interface;

namespace VGProducts.Repository.Implementation
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CountryRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<AddCountryDto> AddCountryAsync(AddCountryDto addCountryDto)
        {
            var data = new Country
            {
                CountryName = addCountryDto.CountryName,
                IsActive = IsActive.Active,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
            };
            await applicationDbContext.Country.AddAsync(data);
            await applicationDbContext.SaveChangesAsync();
            return new AddCountryDto
            {
                CountryName = data.CountryName
            };

        }

        public async Task<string> DeleteCountryAsync(Guid id)
        {
            var country = await applicationDbContext.Country.FirstOrDefaultAsync(c => c.CountryId == id);
            if (country == null)
            {
                return "Country Not Found";
            }
            country.IsActive = IsActive.Inactive;
            country.UpdatedAt = DateTime.UtcNow;
            country.IsDeleted = true;

            await applicationDbContext.SaveChangesAsync();
            return "Country deleted successfully";
        }

        public async Task<List<CountryDto>> GetAllCountryAsync()
        {
            var country = await applicationDbContext.Country.ToListAsync();
            return country.Select(c => new CountryDto
            {
                CountryId = c.CountryId,
                CountryName = c.CountryName,
                IsActive = c.IsActive.ToString(),
                CreatedAt = c.CreatedAt
            }).ToList();
        }

        public async Task<CountryDto> GetCountryById(Guid id)
        {
            var country = await applicationDbContext.Country.Where(c => c.CountryId == id).FirstOrDefaultAsync();
            return new CountryDto
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName,
                IsActive = country.IsActive.ToString(),
                CreatedAt = country.CreatedAt
                
            };
        }
    }
}
