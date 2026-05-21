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
    public class CityBusiness : ICityBusiness
    {
        private readonly ICityRepository cityRepository;

        public CityBusiness(ICityRepository cityRepository) 
        {
            this.cityRepository = cityRepository;
        }
        public async Task<AddCityDto> AddCityAsync(AddCityDto addCityDto)
        {
            return await cityRepository.AddCityAsync(addCityDto);
        }

        public async Task<string> DeleteCityAsync(Guid id)
        {
            return await cityRepository.DeleteCityAsync(id);
        }

        public async Task<List<CityDto>> GetAllCityAsync()
        {
            return await cityRepository.GetAllCityAsync();
        }

        public async Task<CityDto> GetCityByIdAsync(Guid Stateid)
        {
            return await cityRepository.GetCityByIdAsync(Stateid);
        }
    }
}
