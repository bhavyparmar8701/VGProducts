using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Repository.Implementation;
using VGProducts.Repository.Interface;

namespace VGProducts.Business.Implementation
{
    public class SubCategoryBusiness : ISubCategoryBusiness
    {
        private readonly ISubCategoryRepository _subCategoryRepository;

        public SubCategoryBusiness(ISubCategoryRepository subCategoryRepository)
        {
            this._subCategoryRepository = subCategoryRepository;
        }
        public async Task<AddSubCategoryDto> AddSubCategoryAsync(AddSubCategoryDto addSubCategoryDto)
        {
            return await _subCategoryRepository.AddSubCategoryAsync(addSubCategoryDto);
        }
        public async Task<List<SubCategoryDto>> GetAllSubCategoryAsync()
        {
            return await _subCategoryRepository.GetAllSubCategoryAsync();
        }

        public async Task<string> DeleteSubCategoryAsync(Guid id)
        {
            return await _subCategoryRepository.DeleteSubCategoryAsync(id);
        }

        public async Task<string> UpdateSubCategoryAsync(Guid id, UpdateSubCategoryDto updateSubCategoryDto)
        {
            return await _subCategoryRepository.UpdateSubCategoryAsync(id , updateSubCategoryDto);
        }
    }
}
