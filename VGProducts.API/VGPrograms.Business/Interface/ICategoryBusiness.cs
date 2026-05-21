
using VGProducts.Entities.DTOs;

namespace VGProducts.Business.Interface
{
    public interface ICategoryBusiness
    {
        Task<AddCategoryDto> AddCategoryAsync(AddCategoryDto addCategoryDto);
        Task<List<CategoryDto>> GetAllCategoryAsync();
        Task<string> DeleteCategoryAsync(Guid id);
        Task<CategoryDto> UpdateCategoryAsync(Guid id, UpdateCategoryDto updateCategoryDto);
    }
}
