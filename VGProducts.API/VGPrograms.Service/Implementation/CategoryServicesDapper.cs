
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Service.Interface;

namespace VGProducts.Service.Implementation
{
    public class CategoryServicesDapper : ICategoryServicesDapper
    {
        private readonly ICategoryBusinessDapper categoryBusinessDapper;
        private readonly IMapper _mapper;

        public CategoryServicesDapper(ICategoryBusinessDapper categoryBusinessDapper , IMapper mapper)
        {
            this.categoryBusinessDapper = categoryBusinessDapper;
            _mapper = mapper;
        }

        public async Task<CategoryDapperDto> CreateAsync(AddCategoryDapperDto addCategoryDapperDto)
        {
            var category = _mapper.Map<Category>(addCategoryDapperDto);
            category.CategoryId = Guid.NewGuid();
            var result = await categoryBusinessDapper.CreateAsync(addCategoryDapperDto);   
            return _mapper.Map<CategoryDapperDto>(result);
        }

        public async Task<bool> DeleteAsync(Guid categoryId)
        {
            return await categoryBusinessDapper.DeleteAsync(categoryId);
        }

        public async Task<IEnumerable<CategoryDapperDto>> GetAllAsync(IsActive? isActive)
        {
            return await categoryBusinessDapper.GetAllAsync(isActive);
        }
   
    }
}
