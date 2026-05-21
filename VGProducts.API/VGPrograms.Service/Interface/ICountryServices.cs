using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;

namespace VGProducts.Service.Interface
{
    public interface ICountryServices
    {
       Task<AddCountryDto> AddCountryAsync(AddCountryDto addCountryDto);
        Task<List<CountryDto>> GetAllCountryAsync();
        Task<string> DeleteCountryAsync(Guid id);
        Task<CountryDto> GetCountryById(Guid id);
    }
}
