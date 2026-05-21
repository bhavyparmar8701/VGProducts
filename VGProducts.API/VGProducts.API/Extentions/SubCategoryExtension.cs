using Microsoft.AspNetCore.Mvc;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Implementation;
using VGProducts.Service.Interface;

namespace VGProducts.API.Extentions
{
    public static class SubCategoryExtension
    {
        // Call this from Program.cs: app.MapSubCategoryEndpoints(); 
        public static RouteGroupBuilder MapSubCategoryRoute(this RouteGroupBuilder builder)
        {
            builder.MapPost("/subcategory", AddSubCategoryAsync)
                   .RequireAuthorization("CreateSubCategory")
                   .DisableAntiforgery()
                   .WithName("addsubcategory")
                   .WithOpenApi();

            builder.MapGet("/getallsubcategory", GetAllSubCategoryAsync)
                   //.RequireAuthorization("ViewSubCategory")
                   .WithName("getallsubcategory")
                   .WithOpenApi();

            builder.MapDelete("/deletesubcategory/{id}", DeleteSubCategoryAsync)
                   .RequireAuthorization("UpdateSubCategory")
                   .WithName("deletesubcategory")
                   .WithOpenApi();

            builder.MapPut("/updatesubcategory/{id}", UpdateSubCategoryAsync)
                    .DisableAntiforgery()
                   .RequireAuthorization("UpdateSubCategory")
                   .WithName("updatesubcategory")
                   .WithOpenApi();

            return builder;
        }
        private static async Task<IResult> AddSubCategoryAsync([FromServices] ISubCategoryServices subCategoryServices,[FromForm] AddSubCategoryDto addSubCategoryDto)
        {

            var errors = ValidationHelper.Validate(addSubCategoryDto);
            if (errors.Any())
            {
                return Results.BadRequest(new
                {
                    Message = "Validation Failed",
                    Errors = errors
                });
            }
            var result = await subCategoryServices.AddSubCategoryAsync(addSubCategoryDto);
            return Results.Created($"/subcategory/{result.SubCategoryName}", result);
        }
        public static async Task<List<SubCategoryDto>> GetAllSubCategoryAsync([FromServices] ISubCategoryBusiness subCategoryBusiness)
        {
            try
            {
                return await subCategoryBusiness.GetAllSubCategoryAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static async Task<IResult> DeleteSubCategoryAsync(Guid id, ISubCategoryServices subCategoryServices)
        {
            var result = await subCategoryServices.DeleteSubCategoryAsync(id);
            return Results.Ok(result);
        }

        public static async Task<IResult> UpdateSubCategoryAsync(Guid id, [FromForm] UpdateSubCategoryDto updateSubCategoryDto,[FromServices] ISubCategoryServices subCategoryServices)
        {


            var result = await subCategoryServices.UpdateSubCategoryAsync(id, updateSubCategoryDto);

            if (result == null)
            {
                return Results.NotFound("SubCategory Not Found");
            }
            return Results.Ok(result);
        }
    }
}
