
using Dapper;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Repository.DataAccess;
using VGProducts.Repository.Interface;

namespace VGProducts.Repository.Implementation
{
    public class CategoryRepositoryDapper : ICategoryRepositoryDapper
    {
        private readonly DapperContext dapperContext;

        public CategoryRepositoryDapper(DapperContext dapperContext)
        {
            this.dapperContext = dapperContext;
        }


        public async Task<IEnumerable<CategoryDapperDto>> GetAllAsync(IsActive? isActive)
        {
            string query = @"Select ""CategoryId"",""CategoryName"",""Description"",""ImageUrl"",""IsActive"",""CreatedAt"" From ""Category"" Where 1 = 1";

            var parameters = new DynamicParameters();
            if (isActive.HasValue)
            {
                query += " AND \"IsActive\" = @IsActive";
                parameters.Add("IsActive", isActive);
            }

            using var connection = dapperContext.CreateConnection();

            return await connection.QueryAsync<CategoryDapperDto>(query,parameters);
        }
        public async Task<CategoryDapperDto> CreateAsync(AddCategoryDapperDto addCategoryDapperDto)
        {
            var categoryId = Guid.NewGuid();

            string query = @"
        INSERT INTO ""Category""
        (""CategoryId"", ""CategoryName"", ""Description"", ""ImageUrl"", ""IsActive"", ""CreatedAt"")
        VALUES
        (@CategoryId, @CategoryName, @Description, @ImageUrl, 1, CURRENT_TIMESTAMP)";

            using var connection = dapperContext.CreateConnection();

            await connection.ExecuteAsync(query, new
            {
                CategoryId = categoryId,
                addCategoryDapperDto.CategoryName,
                addCategoryDapperDto.Description,
                addCategoryDapperDto.ImageUrl
            });

            return new CategoryDapperDto
            {
                CategoryId = categoryId,
                CategoryName = addCategoryDapperDto.CategoryName,
                Description = addCategoryDapperDto.Description,
                ImageUrl = addCategoryDapperDto.ImageUrl
            };
        }

        public async Task<bool> DeleteAsync(Guid categoryId)
        {
            string query = @"UPDATE ""Category"" SET ""IsActive"" = 0 Where ""CategoryId"" = @CategoryId ";
            using var connection = dapperContext.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                CategoryId = categoryId,
            });
            return rowsAffected > 0;
        }


    }
}
