using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Repository.Interface;

namespace VGProducts.Business.Implementation
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryBusiness(ICategoryRepository categoryRepository) 
        {
            this._categoryRepository = categoryRepository;
        }
        public async Task<AddCategoryDto> AddCategoryAsync(AddCategoryDto addCategoryDto)
        {
            
            return await _categoryRepository.AddCategoryAsync(addCategoryDto);
            
        }

        public async Task<List<CategoryDto>> GetAllCategoryAsync()
        {
            return await _categoryRepository.GetAllCategoryAsync();
        }

        public async Task<string> DeleteCategoryAsync(Guid id)
        {
            return await _categoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(Guid id,UpdateCategoryDto updateCategoryDto)
        {
            return await _categoryRepository.UpdateCategoryAsync(id, updateCategoryDto);
        }
    }
}
