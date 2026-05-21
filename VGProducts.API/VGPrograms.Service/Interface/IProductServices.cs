using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;

namespace VGProducts.Service.Interface
{
    public interface IProductServices
    {
        Task<AddProductDto> AddProductAsync(AddProductDto addProductDto);
        Task<List<ProductDto>> GetAllProductAsync();
        Task<string> DeleteProductAsync(Guid id);
        Task<string> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto);
        Task<ProductDto> GetByIdProductAsync(Guid id);
    }
}
