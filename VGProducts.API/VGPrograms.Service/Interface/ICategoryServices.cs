using VGProducts.Entities.DTOs;

namespace VGProducts.Service.Interface
{
    public interface ICategoryServices
    {
        Task<AddCategoryDto> AddCategoryAsync(AddCategoryDto addCategoryDto);
        Task<List<CategoryDto>> GetAllCategoryAsync();
        Task<string> DeleteCategoryAsync(Guid id);
        Task<CategoryDto> UpdateCategoryAsync(Guid id,UpdateCategoryDto updateCategoryDto);
    }
}
