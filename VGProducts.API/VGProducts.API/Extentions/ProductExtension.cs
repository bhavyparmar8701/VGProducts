using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NLog;
using NLog.Web;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.API.Extentions
{
    public static class ProductExtension
    {
        private static NLog.Logger logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        // Call this from Program.cs: app.MapProductEndpoints();
        public static RouteGroupBuilder MapProductRoute(this RouteGroupBuilder builder)
        {
            builder.MapPost("/product", AddProductAsync)
                   .RequireAuthorization("CreateProduct")
                   .DisableAntiforgery()
                   .WithName("addproduct")
                   .WithOpenApi();

            builder.MapGet("/getallproduct", GetAllProductAsync)
                   //.RequireAuthorization("ViewProduct")
                   .WithName("getallproduct")
                   .WithOpenApi();

            builder.MapDelete("/deleteproduct/{id}", DeleteProductAsync)
                   .RequireAuthorization("DeleteProduct")
                   .WithName("deleteproduct")
                   .WithOpenApi();

            builder.MapPut("/updateproduct/{id}", UpdateProductAsync)
                    .DisableAntiforgery()
                   .RequireAuthorization("UpdateProduct")
                   .WithName("updateproduct")
                   .WithOpenApi();

            builder.MapGet("/getproductbyid/{id}", GetProductByIdAsync)
                   .RequireAuthorization("GetProductById") 
                   .WithName("getproductbyid")
                   .WithOpenApi();

            return builder;
        }

        private static async Task<IResult> AddProductAsync([FromServices] IProductServices productServices,[FromForm] AddProductDto addProductDto )
        {
            try
            {
                if (addProductDto == null)
                    return Results.BadRequest("Invalid request body");

                var errors = ValidationHelper.Validate(addProductDto);

                if (errors.Any())
                {
                    return Results.BadRequest(new
                    {
                        Message = "Validation Failed",
                        Errors = errors
                    });
                }

                var result = await productServices.AddProductAsync(addProductDto);

                return Results.Created($"/product/{result.ProductName}", result);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return TypedResults.Problem("Internal Server Error");
            }
        }
        private static async Task<IResult> GetAllProductAsync([FromServices] IProductServices productServices)
        {
            try
            {
                var result = await productServices.GetAllProductAsync();
                return TypedResults.Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return TypedResults.NotFound();
            }
        }

        private static async Task<IResult> DeleteProductAsync(Guid id, [FromServices] IProductServices productServices)
        {
            try
            {
                var result = await productServices.DeleteProductAsync(id);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return TypedResults.NotFound();
            }
        }
        private static async Task<IResult> UpdateProductAsync(Guid id,[FromForm] UpdateProductDto updateProductDto, [FromServices] IProductServices productServices)
        {
            try
            {
                var result = await productServices.UpdateProductAsync(id, updateProductDto);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Results.NotFound();
            }
        }

        private static async Task<IResult> GetProductByIdAsync(Guid id, IProductServices productServices)
        {
            try
            {
                var result = await productServices.GetByIdProductAsync(id);
                if (result == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Results.NotFound();
            }

        }
    }
}
