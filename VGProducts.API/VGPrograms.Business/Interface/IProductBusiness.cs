using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;

namespace VGProducts.Business.Interface
{
    public interface IProductBusiness
    {
        Task<AddProductDto> AddProductAsync(AddProductDto addProductDto);
        Task<List<ProductDto>> GetAllProductAsync();
        Task<string> DeleteProductAsync(Guid id);
        Task<string> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto);
        Task<ProductDto> GetByIdProductAsync(Guid id);
    }
}
