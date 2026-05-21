using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.Service.Implementation
{
    public class CityServices : ICityServices
    {
        private readonly ICityBusiness cityBusiness;

        public CityServices(ICityBusiness cityBusiness)
        {
            this.cityBusiness = cityBusiness;
        }

        public async Task<AddCityDto> AddCityAsync(AddCityDto addCityDto)
        {
            return await cityBusiness.AddCityAsync(addCityDto);
        }

        public async Task<string> DeleteCityAsync(Guid id)
        {
            return await cityBusiness.DeleteCityAsync(id);
        }

        public async Task<List<CityDto>> GetAllCityAsync()
        {
            return await cityBusiness.GetAllCityAsync();
        }

        public async Task<CityDto> GetCityByIdAsync(Guid Stateid)
        {
            return await cityBusiness.GetCityByIdAsync(Stateid);
        }
    }
}
