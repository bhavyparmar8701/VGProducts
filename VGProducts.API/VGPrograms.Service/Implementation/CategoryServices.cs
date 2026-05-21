
using VGProducts.Business.Implementation;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.Service.Implementation
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryBusiness _categoryBusiness;

        public CategoryServices(ICategoryBusiness categoryBusiness)
        {
            this._categoryBusiness = categoryBusiness;
        }
        public async Task<AddCategoryDto> AddCategoryAsync(AddCategoryDto addCategoryDto)
        {
            return await _categoryBusiness.AddCategoryAsync(addCategoryDto);
        }
        public async Task<List<CategoryDto>> GetAllCategoryAsync()
        {
            return await _categoryBusiness.GetAllCategoryAsync();
        }
        public async Task<string> DeleteCategoryAsync(Guid id)
        {
           return await _categoryBusiness.DeleteCategoryAsync(id);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(Guid id,UpdateCategoryDto updateCategoryDto)
        {
            return await _categoryBusiness.UpdateCategoryAsync(id, updateCategoryDto);
        }
    }
}
