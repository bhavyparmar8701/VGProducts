using System.Net.NetworkInformation;
using VGProducts.Entities.DTOs;

namespace VGProducts.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<AddCategoryDto> AddCategoryAsync(AddCategoryDto addCategoryDto);
        Task<List<CategoryDto>> GetAllCategoryAsync();
        Task<string> DeleteCategoryAsync(Guid id);
        Task<CategoryDto> UpdateCategoryAsync(Guid id,UpdateCategoryDto updateCategoryDto);
    }
}
