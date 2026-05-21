using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;

namespace VGProducts.Business.Interface
{
    public interface ICityBusiness
    {
        Task<AddCityDto> AddCityAsync(AddCityDto addCityDto);
        Task<List<CityDto>> GetAllCityAsync();
        Task<string> DeleteCityAsync(Guid id);
        Task<CityDto> GetCityByIdAsync(Guid Stateid);
    }
}
