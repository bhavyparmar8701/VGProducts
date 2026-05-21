using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.Service.Implementation
{
    public class ProductServices : IProductServices
    {
        private readonly IProductBusiness productBusiness;
        private readonly IDistributedCache distributedCache;

        public ProductServices(IProductBusiness productBusiness,IDistributedCache distributedCache) 
        {
            this.productBusiness = productBusiness;
            this.distributedCache = distributedCache;
        }
        public async Task<AddProductDto> AddProductAsync(AddProductDto addProductDto)
        {
            return await productBusiness.AddProductAsync(addProductDto);
        }

        public async Task<string> DeleteProductAsync(Guid id)
        {
            return await  productBusiness.DeleteProductAsync(id);
        }

        public async Task<List<ProductDto>> GetAllProductAsync()
        {
            return await productBusiness.GetAllProductAsync();
        }

        public async Task<ProductDto> GetByIdProductAsync(Guid id)
        {
            return await productBusiness.GetByIdProductAsync(id);
        }

        public async Task<string> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto)
        {
            return await productBusiness.UpdateProductAsync(id, updateProductDto);
        }
    }
}
