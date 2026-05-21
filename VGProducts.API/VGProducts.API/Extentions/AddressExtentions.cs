using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Implementation;
using VGProducts.Service.Interface;

namespace VGProducts.API.Extentions
{
    public static class AddressExtentions
    {
        public static RouteGroupBuilder MapAddressRoute(this RouteGroupBuilder builder)
        {
            builder.MapPost("/addaddress", AddAddressAsync)
                   .RequireAuthorization("CreateAddress")
                   .WithName("addaddress")
                   .WithOpenApi();

            builder.MapGet("/getAddress", GetAllAddressAsync)
                .RequireAuthorization("GetAddress")
                .WithName("getAddress")
                .WithOpenApi();

            builder.MapDelete("/deleteAddress/{addressId}/{userId}", DeleteAddressAsync)
                .RequireAuthorization("DeleteAddress")
                .WithName("deleteAddress")
                .WithOpenApi();

            builder.MapGet("/getAddressById/{addressId}/{userId}", GetAddressById)
                .RequireAuthorization("GetAddressById")
                .WithName("getAddressById")
                .WithOpenApi();

            builder.MapPut("/updateAddress/{addressId}/{userId}", UpdateAddressAsync)
                .RequireAuthorization("UpdateAddress")
                .WithName("updateAddress")
                .WithOpenApi();

            return builder;
        }
        private static async Task<IResult> AddAddressAsync([FromServices] IAddressServices addressServices, [FromBody] AddAddressDto addAddressDto)
        {
            
            var errors = ValidationHelper.Validate(addAddressDto);
            if (errors.Any())
            {
                return Results.BadRequest(new
                {
                    Message = "Validation Failed",
                    Errors = errors
                });
            }
            var result = await addressServices.AddAddressAsync(addAddressDto);

            return Results.Ok(result);
        }
        private static async Task<IResult> GetAllAddressAsync( [FromServices] IAddressServices addressServices,Guid userId)
        {
            
            var result = await addressServices.GetAllAddressAsync(userId);

            return Results.Ok(result);
        }
        private static async Task<IResult> DeleteAddressAsync( [FromServices] IAddressServices addressServices, Guid AddressId, Guid userId)
        {
            var result = await addressServices.DeleteAddressAsync(AddressId, userId);
            return Results.Ok(result);
        }
        private static async Task<IResult> GetAddressById( [FromServices] IAddressServices addressServices, Guid addressId, Guid userId)
        {
            
            var result = await addressServices.GetAddressByIdAsync(addressId, userId);
            return Results.Ok(new
            {
                Message = result
            });
        }
        private static async Task<IResult> UpdateAddressAsync([FromServices] IAddressServices addressServices,[FromRoute] Guid addressId, [FromBody] UpdateAddressDto updateAddressDto, Guid userId)
        {
            
            var errors = ValidationHelper.Validate(updateAddressDto);
            if (errors.Any())
            {
                return Results.BadRequest(new
                {
                    Message = "Validation Failed",
                    Errors = errors
                });
            }
            var result = await addressServices.UpdateAddressAsync(addressId, updateAddressDto, userId);
            return Results.Ok(result);
        }
    }
}
