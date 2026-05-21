using VGProducts.Entities.DTOs;

namespace VGProducts.Business.Interface
{
    public interface ISubCategoryBusiness
    {
        Task<AddSubCategoryDto> AddSubCategoryAsync(AddSubCategoryDto addSubCategoryDto);
        Task<List<SubCategoryDto>> GetAllSubCategoryAsync();
        Task<string> DeleteSubCategoryAsync(Guid id);
        Task<string> UpdateSubCategoryAsync(Guid id, UpdateSubCategoryDto updateSubCategoryDto);
    }
}
