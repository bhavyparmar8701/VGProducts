using VGProducts.Entities.DTOs;

namespace VGProducts.Repository.Interface
{
    public interface ISubCategoryRepository
    {
        Task<AddSubCategoryDto> AddSubCategoryAsync(AddSubCategoryDto addSubCategoryDto);
        Task<List<SubCategoryDto>> GetAllSubCategoryAsync();
        Task<string> DeleteSubCategoryAsync(Guid id);
        Task<string> UpdateSubCategoryAsync(Guid id,UpdateSubCategoryDto updateSubCategoryDto);
    }
}
