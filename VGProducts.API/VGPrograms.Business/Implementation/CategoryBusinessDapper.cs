
using AutoMapper;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Repository.Interface;

namespace VGProducts.Business.Implementation
{
    public class CategoryBusinessDapper : ICategoryBusinessDapper
    {
        private readonly ICategoryRepositoryDapper categoryRepositoryDapper;

        public CategoryBusinessDapper(ICategoryRepositoryDapper categoryRepositoryDapper)
        {
            this.categoryRepositoryDapper = categoryRepositoryDapper;
        }

        public async Task<CategoryDapperDto> CreateAsync(AddCategoryDapperDto addCategoryDapperDto)
        {
            return await categoryRepositoryDapper.CreateAsync(addCategoryDapperDto);
        }

        public async Task<bool> DeleteAsync(Guid categoryId)
        {
            return await categoryRepositoryDapper.DeleteAsync(categoryId);
        }

        public async Task<IEnumerable<CategoryDapperDto>> GetAllAsync(IsActive? isActive)
        {
           return await categoryRepositoryDapper.GetAllAsync(isActive);
        }

     
    }
}
