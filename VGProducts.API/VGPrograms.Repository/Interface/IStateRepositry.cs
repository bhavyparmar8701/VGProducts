using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;

namespace VGProducts.Repository.Interface
{
    public interface IStateRepositry
    {
        Task<AddCategoryDto> AddStateAsync(AddCategoryDto addCategoryDto);
    }
}
