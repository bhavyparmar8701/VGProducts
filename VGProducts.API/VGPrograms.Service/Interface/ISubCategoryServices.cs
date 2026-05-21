using VGProducts.Entities.DTOs;
namespace VGProducts.Service.Interface
{
    public interface ISubCategoryServices
    {
        Task<AddSubCategoryDto> AddSubCategoryAsync(AddSubCategoryDto addSubCategoryDto);
        Task<List<SubCategoryDto>> GetAllSubCategoryAsync();
        Task<string> DeleteSubCategoryAsync(Guid id);
        Task<string> UpdateSubCategoryAsync(Guid id,UpdateSubCategoryDto updateSubCategoryDto);
    }
}
