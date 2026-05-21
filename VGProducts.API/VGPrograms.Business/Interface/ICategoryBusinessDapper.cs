using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;

namespace VGProducts.Business.Interface
{
    public interface ICategoryBusinessDapper
    {
        Task<IEnumerable<CategoryDapperDto>> GetAllAsync(IsActive? isActive);
        Task<CategoryDapperDto> CreateAsync(AddCategoryDapperDto addCategoryDapperDto);
        Task<bool> DeleteAsync(Guid categoryId);
    }
}
