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
    public class CountryBusiness : ICountryBusiness
    {
        private readonly ICountryRepository countryRepositry;

        public CountryBusiness(ICountryRepository countryRepositry)
        {
            this.countryRepositry = countryRepositry;
        }
        public async Task<AddCountryDto> AddCountryAsync(AddCountryDto addCountryDto)
        {
            return await countryRepositry.AddCountryAsync(addCountryDto);
        }

        public async Task<string> DeleteCountryAsync(Guid id)
        {
            return await countryRepositry.DeleteCountryAsync(id);
        }

        public async Task<List<CountryDto>> GetAllCountryAsync()
        {
            return await countryRepositry.GetAllCountryAsync();
        }

        public async Task<CountryDto> GetCountryById(Guid id)
        {
            return await countryRepositry.GetCountryById(id);
        }
    }
}
