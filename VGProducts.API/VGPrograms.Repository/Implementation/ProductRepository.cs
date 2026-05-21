using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Repository.DataAccess;
using VGProducts.Repository.Interface;

namespace VGProducts.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<string> UploadImageAsync(IFormFile formFile)
        {
            if(formFile == null || formFile.Length == 0)
            {
                return "Invalid File";
            }
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var ext = Path.GetExtension(formFile.FileName);
            if (!allowedExtensions.Contains(ext))
            {
                return "Only jpg, jpeg, png, gif allowed";
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Product");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = Guid.NewGuid().ToString() + ext;
            var filePath = Path.Combine(folderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            return "/Images/Product/" + fileName;
        }
        public async Task<AddProductDto> AddProductAsync(AddProductDto addProductDto)
        {
            string imageUrl = await UploadImageAsync(addProductDto.ImageUrl);
            var data = new Product
            {
                ProductName = addProductDto.ProductName,
                Description = addProductDto.Description,
                ShortDescription = addProductDto.ShortDescription,
                Price = addProductDto.Price,
                Stock = addProductDto.Stock,
                SKU = addProductDto.SKU,
                ImageUrl = imageUrl,
                SubCategoryId = addProductDto.SubCategoryId,
                Reting = 0,
                ReviewCount = 0,
                IsActive = IsActive.Active,
                IsDeleted = false,
            };

            await applicationDbContext.Product.AddAsync(data);
            await applicationDbContext.SaveChangesAsync();
            return new AddProductDto
            {

                ProductName = data.ProductName,
                Description = data.Description,
                ShortDescription = data.ShortDescription,
                Price = data.Price,
                Stock = data.Stock,
                SKU = data.SKU,
                SubCategoryId = data.SubCategoryId
            };

        }
        public async Task<List<ProductDto>> GetAllProductAsync()
        {
            var product = await applicationDbContext.Product.Where(p => p.IsActive == IsActive.Active).ToListAsync();

            return product.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Description = p.Description,
                ShortDescription = p.ShortDescription,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Stock = p.Stock,
                SKU = p.SKU,
                SubCategoryId = p.SubCategoryId,
                IsActive = p.IsActive.ToString(),
                Reting = p.Reting,
                ReviewCount = p.ReviewCount
            }).ToList();
        }

        public async Task<string> DeleteProductAsync(Guid id)
        {
            var product = await applicationDbContext.Product
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return "Product Not Found";
            }
            product.IsActive = IsActive.Inactive;
            product.UpdatedAt = DateTime.UtcNow;
            product.IsDeleted = true;

            await applicationDbContext.SaveChangesAsync();
            return "Product Delete Successfully";
        }

        public async Task<string> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto)
        {
            var product = await applicationDbContext.Product.FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return null;
            }

            string imageUrl = null;

            if (updateProductDto.ImageUrl != null)
            {
                imageUrl = await UploadImageAsync(updateProductDto.ImageUrl); 
            }

            product.ProductName = updateProductDto.ProductName;
            product.Description = updateProductDto.Description;
            product.ShortDescription = updateProductDto.ShortDescription;
            product.Price = updateProductDto.Price;
            if (imageUrl != null)
            {
                product.ImageUrl = imageUrl;
            }
            product.Stock = updateProductDto.Stock;
            product.SKU = updateProductDto.SKU;
            product.SubCategoryId = updateProductDto.SubCategoryId;
            product.IsActive = updateProductDto.IsActive;
            product.UpdatedAt = DateTime.UtcNow;

            await applicationDbContext.SaveChangesAsync();

            return "Product Update Successfully";
        }

        public async Task<ProductDto> GetByIdProductAsync(Guid id)
        {
           var product = await applicationDbContext.Product.Include(p => p.Reviews).FirstOrDefaultAsync(p => p.ProductId == id && p.IsActive == IsActive.Active);  
            if (product == null)
            {
                return null;
            }
            return new ProductDto
            { 
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                Price = product.Price,
                Stock = product.Stock,
                SKU = product.SKU,
                ImageUrl = product.ImageUrl,
                SubCategoryId = product.SubCategoryId, 
                IsActive = product.IsActive.ToString(),
                Reting = product.Reting,
                ReviewCount = product.ReviewCount,

                Reviews = product.Reviews.Select(r => new  ReviewDto
                {
                    UserId = r.UserId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt
                }).ToList()
            };
        }
    }
}
