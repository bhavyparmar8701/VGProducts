using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;

namespace VGProducts.Business.Interface
{
    public interface ICountryBusiness
    {
        Task<AddCountryDto> AddCountryAsync(AddCountryDto addCountryDto);
        Task<List<CountryDto>> GetAllCountryAsync();
        Task<string> DeleteCountryAsync(Guid id);
        Task<CountryDto> GetCountryById(Guid id);
    }
}
