using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Repository.Interface;

namespace VGProducts.Business.Implementation
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepository _productRepository;

        public ProductBusiness(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
        public async Task<AddProductDto> AddProductAsync(AddProductDto addProductDto)
        {
            return await _productRepository.AddProductAsync(addProductDto);
        }

        public async Task<string> DeleteProductAsync(Guid id)
        {
            return await _productRepository.DeleteProductAsync(id);
        }

        public async Task<List<ProductDto>> GetAllProductAsync()
        {
            return await _productRepository.GetAllProductAsync();
        }

        public async Task<ProductDto> GetByIdProductAsync(Guid id)
        {
            return await _productRepository.GetByIdProductAsync(id);
        }

        public async Task<string> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto)
        {
            return await _productRepository.UpdateProductAsync(id , updateProductDto);
        }
    }
}
