using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;

namespace VGProducts.Repository.Interface
{
    public interface ICategoryRepositoryDapper
    {
        Task<IEnumerable<CategoryDapperDto>> GetAllAsync(IsActive? isActive);
        Task<CategoryDapperDto> CreateAsync(AddCategoryDapperDto addCategoryDapperDto);
        Task<bool> DeleteAsync(Guid categoryId);
    }
}
