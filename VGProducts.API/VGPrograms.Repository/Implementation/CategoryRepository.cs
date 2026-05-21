using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Repository.DataAccess;
using VGProducts.Repository.Interface;

namespace VGProducts.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }


        public async Task<AddCategoryDto> AddCategoryAsync(AddCategoryDto addCategoryDto)
        {
            var data = new Category
            {
                CategoryName = addCategoryDto.CategoryName,
                Description = addCategoryDto.Description,
                ImageUrl = addCategoryDto.ImageUrl,
                IsActive = IsActive.Active,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await applicationDbContext.Category.AddAsync(data);
            await applicationDbContext.SaveChangesAsync();
            return new AddCategoryDto
            {
                CategoryName = data.CategoryName,
                Description = data.Description,
                ImageUrl = data.ImageUrl
            };

        }
        public async Task<List<CategoryDto>> GetAllCategoryAsync()
        {

            var category = await applicationDbContext.Category.ToListAsync();
            return category.Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                IsActive = c.IsActive.ToString(),
                CreatedAt = c.CreatedAt
            }).ToList();


        }

        public async Task<string> DeleteCategoryAsync(Guid id)
        {
            var category = await applicationDbContext.Category
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return "Category Not Found";
            }
            category.IsActive = IsActive.Inactive;
            category.UpdatedAt = DateTime.UtcNow;
            category.IsDeleted = true;

            await applicationDbContext.SaveChangesAsync();
            return "Category Deletes Successfully";
        }

        public async Task<CategoryDto> UpdateCategoryAsync(Guid id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await applicationDbContext.Category.Where(c => c.CategoryId == id).FirstOrDefaultAsync();

            if (category == null)
            {
                return null;
            }
            category.Description = updateCategoryDto.Description;
            category.ImageUrl = updateCategoryDto.ImageUrl;
            category.IsActive = updateCategoryDto.IsActive;
            category.UpdatedAt = DateTime.UtcNow;

            await applicationDbContext.SaveChangesAsync();
            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description,
                ImageUrl = category.ImageUrl,
                IsActive = category.IsActive.ToString(),
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
