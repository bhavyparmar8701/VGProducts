using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;


using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VGProducts.API.Extentions
{
    public static class FavouritesExtention
    {
        public static RouteGroupBuilder MapFavouriesRoute(this RouteGroupBuilder builder)
        {
            builder.MapPost("/addFavourites", AddFavouritesAsync)
                   .RequireAuthorization("CreateFavourites")
                   .WithName("addFavourites")
                   .WithOpenApi();

            builder.MapGet("/viewFavourites", GetAllFavouritesAsync)
                   .RequireAuthorization("ViewFavourites")
                   .WithName("viewFavourites")
                   .WithOpenApi();

            builder.MapDelete("/deleteFavourites/{productId}", DeleteFavouritesAsync)
                   .RequireAuthorization("DeleteFavourites")
                   .WithName("deleteFavourites")
                   .WithOpenApi();

            return builder;
        }

        private static async Task<IResult> AddFavouritesAsync(
            [FromServices] IFavouritesServices favouritesServices,
            [FromBody] AddFavouritesDto addFavouritesDto)
        {
            var errors = ValidationHelper.Validate(addFavouritesDto);
            if (errors.Any())
            {
                return Results.BadRequest(new
                {
                    Message = "Validation Failed",
                    Errors = errors
                });
            }

            var result = await favouritesServices.AddFavouritesAsync(addFavouritesDto);
            return Results.Ok(result);
        }

        // ✅ userId from query
        private static async Task<IResult> GetAllFavouritesAsync([FromServices] IFavouritesServices favouritesServices,[FromQuery] Guid userId)
        {
            if (userId == Guid.Empty)
                return Results.BadRequest("UserId is required");

            var result = await favouritesServices.GetAllFavouritesAsync(userId);
            return Results.Ok(result);
        }

        // ✅ id from route, userId from query
        private static async Task<IResult> DeleteFavouritesAsync(Guid productId, [FromServices] IFavouritesServices favouritesServices,[FromQuery] Guid userId)
        {
            if (userId == Guid.Empty)
                return Results.BadRequest("UserId is required");

            var result = await favouritesServices.DeleteFavouritesAsync(productId, userId);
            return Results.Ok(result);
        }
    }
}
