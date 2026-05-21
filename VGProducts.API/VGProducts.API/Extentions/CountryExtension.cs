using Microsoft.AspNetCore.Mvc;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.API.Extentions
{
    public static class CountryExtension
    {
        public static RouteGroupBuilder MapCountryRoute(this RouteGroupBuilder builder)
        {
            builder.MapPost("/addcountry", AddCountryAsync)
                   .RequireAuthorization("CreateCountry")
                   .WithName("addcountry")
                   .WithOpenApi();

            builder.MapGet("/getCountry", GetAllCountryAsync)
                .RequireAuthorization("GetCountry")
                .WithName("getCountry")
                .WithOpenApi();

            builder.MapDelete("/deleteCountry/{id}", DeleteCountryAsync)
                .RequireAuthorization("DeleteCountry")
                .WithName("deleteCountry")
                .WithOpenApi();

            builder.MapGet("/getCountryById/{id}", GetCountryById)
                .RequireAuthorization("GetCountryById")
                .WithName("getCountryById")
                .WithOpenApi();

            return builder;
        }
        private static async Task<IResult> AddCountryAsync([FromServices] ICountryServices countryServices, [FromBody] AddCountryDto addCountryDto)
        {
            if (countryServices == null || string.IsNullOrWhiteSpace(addCountryDto.CountryName))
            {
                return Results.BadRequest(new
                {
                    Message="Invalid input data."
                });
            }
            var result = await countryServices.AddCountryAsync(addCountryDto);
            return Results.Ok(new
            {
                Message = result
            });
        }
        private static async Task<List<CountryDto>> GetAllCountryAsync([FromServices] ICountryServices countryServices)
        {
            return await countryServices.GetAllCountryAsync();
        }
        private static async Task<IResult> DeleteCountryAsync([FromServices] ICountryServices countryServices, Guid id)
        {
            var result = await countryServices.DeleteCountryAsync(id);
            return Results.Ok(new
            {
                Message = result
            });
        }
        private static async Task<IResult> GetCountryById([FromServices] ICountryServices countryServices, Guid id)
        {
            var result = await countryServices.GetCountryById(id);
            return Results.Ok(new
            {
                Message = result
            });
        }
    }
}