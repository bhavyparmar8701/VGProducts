using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.Service.Implementation
{
    public class CountryServices : ICountryServices
    {
        private readonly ICountryBusiness countryBusiness;

        public CountryServices(ICountryBusiness countryBusiness)
        {
            this.countryBusiness = countryBusiness;
        }

        public async Task<AddCountryDto> AddCountryAsync(AddCountryDto addCountryDto)
        {
            return await countryBusiness.AddCountryAsync(addCountryDto);
        }

        public async Task<string> DeleteCountryAsync(Guid id)
        {
            return await countryBusiness.DeleteCountryAsync(id);
        }

        public async Task<List<CountryDto>> GetAllCountryAsync()
        {
            return await countryBusiness.GetAllCountryAsync();
        }

        public async Task<CountryDto> GetCountryById(Guid id)
        {
            return await countryBusiness.GetCountryById(id);
        }
    }
}
