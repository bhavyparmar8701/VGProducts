using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Implementation;
using VGProducts.Service.Interface;

namespace VGProducts.API.Extentions
{
    public static class CartItemExtentios
    {
        public static RouteGroupBuilder MapCartItemRoute(this RouteGroupBuilder builder)
        {
            builder.MapPost("/AddCartItem", AddToCart)
                    .RequireAuthorization("CreateCartItem")
                    .WithName("AddCartItem")
                    .WithOpenApi();

            builder.MapGet("/GetAllCartItem/{userId}", GetAllCartItem)
                    .RequireAuthorization("ViewCartItem")
                    .WithName("GetAllCartItem")
                    .WithOpenApi();
            builder.MapDelete("/DeleteCartItem/{Id}/{userId}", DeleteByIdCartItemAsync)
                    .RequireAuthorization("DeleteCartItemById")
                    .WithName("DeleteCartItem")
                    .WithOpenApi();
            builder.MapDelete("/DeleteAllCartItem/{userId}", DeleteAllCartItemAsync)
                    .RequireAuthorization("DeleteAllCartItemAsync")
                    .WithName("DeleteAllCartItem")
                    .WithOpenApi();
            builder.MapPost("/AddByIdCartItem/{id}/{userId}", AddByIdCartItemAsync) 
                    .RequireAuthorization("AddByIdCartItemAsync")
                    .WithName("AddByIdCartItem")
                    .WithOpenApi();

            return builder;
        }
        private static async Task<IResult> AddToCart( [FromServices] ICartItemServices cartItemServices, [FromBody] AddCartItemDto addCartItemDto)
        {
            if (addCartItemDto == null)
            {
                    return Results.BadRequest(new { Message = "Invalid request body" });
            }


            try
            {
                var result = await cartItemServices.AddToCart(addCartItemDto);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { Message = ex.Message });
            }
        }
        private static async Task<IResult> GetAllCartItem(Guid userId, [FromServices] ICartItemServices cartItemServices)
        {
            
            var result = await cartItemServices.GetAllCartItem(userId);
            if (result == null || result.Items == null || !result.Items.Any())
            {
                return Results.NotFound(new
                {
                    Message = "Cart is empty"
                });
            }
            return Results.Ok(result);
        }
        private static async Task<IResult> DeleteByIdCartItemAsync( [FromServices] ICartItemServices cartItemServices, Guid Id, Guid userId)
        {
            var result = await cartItemServices.DeleteByIdCartItemAsync(Id, userId);
            if (result == "Cart Not Found" || result == "Cart Item Not Found")
            {
                return Results.NotFound(new
                {
                    Message = result
                });

            }

            return Results.Ok(new
            {
                Message = result
            });

        }
        private static async Task<IResult> DeleteAllCartItemAsync([FromServices] ICartItemServices cartItemServices , Guid userId)
        {
            
            var result = await cartItemServices.DeleteAllCartItemAsync(userId);
            if (result == "Cart Not Found" || result == "Cart Item Not Found")
            {
                return Results.NotFound(new
                {
                    Message = result
                });

            }

            return Results.Ok(new
            {
                Message = result
            });
        }
        private static async Task<IResult> AddByIdCartItemAsync([FromServices] ICartItemServices cartItemServices, Guid id, Guid userId)
        {
            
            var result = await cartItemServices.AddByIdCartItemAsync(id, userId);
            if (result == "Cart Not Found" || result == "Cart Item Not Found")
            {
                return Results.NotFound(new
                {
                    Message = result
                });
            }
            return Results.Ok(new
            {
                Message = result
            });
        }
    }

}
