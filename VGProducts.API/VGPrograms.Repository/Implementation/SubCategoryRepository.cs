
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Repository.DataAccess;
using VGProducts.Repository.Interface;



namespace VGProducts.Repository.Implementation
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public SubCategoryRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<string> UploadImageAsync(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
            {
                return "Invalid File";
            }
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var ext = Path.GetExtension(formFile.FileName);
            if (!allowedExtensions.Contains(ext))
            {
                return "Only jpg, jpeg, png, gif allowed";
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/SubCategory");
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
            return "/Images/SubCategory/" + fileName;
        }

        public async Task<AddSubCategoryDto> AddSubCategoryAsync(AddSubCategoryDto addSubCategoryDto)
        {
            string imageUrl = await UploadImageAsync(addSubCategoryDto.ImageUrl);
            var data = new SubCategory
            {

                SubCategoryName = addSubCategoryDto.SubCategoryName,
                CategoryId = addSubCategoryDto.CategoryId,
                Description = addSubCategoryDto.Description,
                Image = imageUrl,
                IsActive = IsActive.Active,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await applicationDbContext.SubCategory.AddAsync(data);
            await applicationDbContext.SaveChangesAsync();

            return new AddSubCategoryDto
            {
                SubCategoryName = data.SubCategoryName,
                CategoryId = data.CategoryId,
                Description = data.Description
            };

        }

        public async Task<List<SubCategoryDto>> GetAllSubCategoryAsync()
        {
            var subCategory = await applicationDbContext.SubCategory.ToListAsync();
            return subCategory.Select(s => new SubCategoryDto
            {
                SubCategoryId = s.SubCategoryId,
                SubCategoryName = s.SubCategoryName,
                CategoryId = s.CategoryId,
                Description = s.Description,
                ImageUrl = s.Image,
                IsActive = s.IsActive.ToString()
            }).ToList();

        }
        public async Task<string> DeleteSubCategoryAsync(Guid id)
        {
            var subCategory = await applicationDbContext.SubCategory
                .FirstOrDefaultAsync(c => c.SubCategoryId == id);
            if (subCategory == null)
            {
                return "Category Not Found";
            }
            subCategory.IsActive = IsActive.Inactive;
            subCategory.UpdatedAt = DateTime.UtcNow;
            subCategory.IsDeleted = true;

            await applicationDbContext.SaveChangesAsync();
            return "SubCategory Deletes Successfully";
        }

        public async Task<string> UpdateSubCategoryAsync(Guid id, UpdateSubCategoryDto updateSubCategoryDto)
        {
            var subcategory = await applicationDbContext.SubCategory.Where(s => s.SubCategoryId == id).FirstOrDefaultAsync();

            if (subcategory == null)
            {
                return "SubCategory Not Found";
            }
            string imageUrl = null;

            if (updateSubCategoryDto.ImageUrl != null)
            {
                imageUrl = await UploadImageAsync(updateSubCategoryDto.ImageUrl);
            }

            subcategory.SubCategoryName = updateSubCategoryDto.SubCategoryName;
            subcategory.Description = updateSubCategoryDto.Description;
            if (imageUrl != null)
            {
                subcategory.Image = imageUrl;
            }
            subcategory.CategoryId = updateSubCategoryDto.CategoryId;
            subcategory.IsActive = IsActive.Active;
            subcategory.UpdatedAt = DateTime.UtcNow;

            await applicationDbContext.SaveChangesAsync();

            return "SubCategory Updated Successfully";
        }
    }
}
