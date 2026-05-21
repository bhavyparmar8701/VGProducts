using Microsoft.AspNetCore.Mvc;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.API.Extentions
{
    public static class CategoeyExtention
    {
        // Call this from Program.cs: app.MapCategoryEndpoints();
        public static RouteGroupBuilder MapCategoryRoute(this RouteGroupBuilder builder)
        {
            builder.MapPost("/addcategory", AddCategoryAsync)
                   .RequireAuthorization("CreateCategory")
                   .WithName("addcategory")
                   .WithOpenApi();

            builder.MapGet("/getallcategory", GetAllCategoryAsync)
                   //.RequireAuthorization("ViewCategory")
                   .WithName("getallcategory")
                   .WithOpenApi();

            builder.MapPut("/deleteCategory/{id}", DeleteCategoryAsync)
                   .RequireAuthorization("DeleteCategory")
                   .WithName("deleteCategory")
                   .WithOpenApi();

            builder.MapPut("/updateCategory/{id}", UpdateCategoryAsync)
                   .RequireAuthorization("UpdateCategory")
                   .WithName("updatecategory")
                   .WithOpenApi();

            return builder;
        }

        private static async Task<IResult> AddCategoryAsync([FromServices] ICategoryServices categoryServices, [FromBody] AddCategoryDto addCategoryDto)
        {

            var errors = ValidationHelper.Validate(addCategoryDto);

            if (errors.Any())
            {
                return Results.BadRequest(new
                {
                    Message = "Validation Failed",
                    Errors = errors
                });
            }

            var result = await categoryServices.AddCategoryAsync(addCategoryDto);

            return Results.Created($"/api/category/{result.CategoryName}", result);

        }
        public static async Task<List<CategoryDto>> GetAllCategoryAsync([FromServices] ICategoryServices categoryServices)
        {
            try
            {
                return await categoryServices.GetAllCategoryAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static async Task<IResult> DeleteCategoryAsync(Guid id, ICategoryServices categoryServices)
        {
            var result = await categoryServices.DeleteCategoryAsync(id);
            return Results.Ok(result);
        }

        public static async Task<IResult> UpdateCategoryAsync(Guid id, UpdateCategoryDto updateCategoryDto, ICategoryServices categoryServices)
        {


            var result = await categoryServices.UpdateCategoryAsync(id, updateCategoryDto);


            if (result == null)
            {
                return Results.NotFound("Category Not Found");
            }
            return Results.Ok(result);
        }
    }
}
