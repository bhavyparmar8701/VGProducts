using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;

namespace VGProducts.Service.Interface
{
    public interface ICategoryServicesDapper
    {
        Task<IEnumerable<CategoryDapperDto>> GetAllAsync(IsActive? isActive);
        Task<CategoryDapperDto> CreateAsync(AddCategoryDapperDto addCategoryDapperDto);
        Task<bool> DeleteAsync(Guid categoryId);
    }
}
