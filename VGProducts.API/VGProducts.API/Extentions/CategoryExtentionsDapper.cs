using Microsoft.AspNetCore.Mvc;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Service.Interface;

namespace VGProducts.API.Extentions
{
    public static class CategoryExtentionsDapper
    {
        public static RouteGroupBuilder MapCategoryRouteDapper(this RouteGroupBuilder builder)
        {
            builder.MapGet("GetAllCategoryDapper", GetAllAsync)
                   .RequireAuthorization("ViewCategory")
                   .WithName("GetAllCategoriesDapper")
                   .WithOpenApi();
            builder.MapPost("CreateCategoryDapper", CreateAsync)
                   .RequireAuthorization("CreateCategory")
                   .WithName("CreateCategoryDapper")
                   .WithOpenApi();

            builder.MapDelete("DeleteCategoryDapper/{id}", DeleteAsync)
                   .RequireAuthorization("DeleteCategory")
                   .WithName("DeleteCategoryDapper")
                   .WithOpenApi();

            return builder;
        }
        public static async Task<IResult> GetAllAsync(IsActive? isActive, [FromServices] ICategoryServicesDapper categoryServicesDapper)
        {
            var result = await categoryServicesDapper.GetAllAsync(isActive);
            return Results.Ok(result);
        }
        public static async Task<IResult> CreateAsync(AddCategoryDapperDto dto, [FromServices] ICategoryServicesDapper categoryServicesDapper)
        {
            var result = await categoryServicesDapper.CreateAsync(dto);
            return Results.Ok(result);
        }
        public static async Task<IResult> DeleteAsync(Guid categoryId, [FromServices] ICategoryServicesDapper categoryServicesDapper)
        {
            var result = await categoryServicesDapper.DeleteAsync(categoryId);
            if (!result)
                return Results.NotFound("Category Not Found");

            return Results.Ok("Category Delete Successfully");
        }
    }
}
