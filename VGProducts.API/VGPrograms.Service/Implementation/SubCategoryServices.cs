using Microsoft.Extensions.Caching.Memory;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.Service.Implementation
{
    public class SubCategoryServices : ISubCategoryServices
    {
        private readonly ISubCategoryBusiness _subCategoryBusiness;
        private readonly IMemoryCache memoryCache;

        public SubCategoryServices(ISubCategoryBusiness subCategoryBusiness,IMemoryCache memoryCache)
        {
            this._subCategoryBusiness = subCategoryBusiness;
            this.memoryCache = memoryCache;
        }
        public async Task<AddSubCategoryDto> AddSubCategoryAsync(AddSubCategoryDto addSubCategoryDto)
        {
            return await _subCategoryBusiness.AddSubCategoryAsync(addSubCategoryDto);
        }
        public async Task<List<SubCategoryDto>> GetAllSubCategoryAsync()
        {
            string cachedkey = "SubCategory_all";

            if(memoryCache.TryGetValue(cachedkey, out List<SubCategoryDto> cachedData))
            {
                return cachedData;
            }

            var data = await _subCategoryBusiness.GetAllSubCategoryAsync();

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            memoryCache.Set(cachedkey, data, cacheOptions);

            return data;
        }

        public async Task<string> DeleteSubCategoryAsync(Guid id)
        {
            return await _subCategoryBusiness.DeleteSubCategoryAsync(id);
        }

        public async Task<string> UpdateSubCategoryAsync(Guid id,UpdateSubCategoryDto updateSubCategoryDto)
        {
            return await _subCategoryBusiness.UpdateSubCategoryAsync(id, updateSubCategoryDto);

            memoryCache.Remove("SubCategory_all");
        }
    }
}
