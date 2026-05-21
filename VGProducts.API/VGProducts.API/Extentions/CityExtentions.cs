using Microsoft.AspNetCore.Mvc;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.API.Extentions
{
    public static class CityExtentions
    {
        public static RouteGroupBuilder MapCityRoute(this RouteGroupBuilder builder)
        {
            builder.MapPost("/addcity", AddCityAsync)
                   .RequireAuthorization("CreateCity")
                   .WithName("addcity")
                   .WithOpenApi();

            builder.MapGet("/getCity", GetAllCityAsync)
                .RequireAuthorization("GetCity")
                .WithName("getCity")
                .WithOpenApi();

            builder.MapDelete("/deleteCity/{id}", DeleteCityAsync)
                .RequireAuthorization("DeleteCity")
                .WithName("deleteCity")
                .WithOpenApi();

            builder.MapGet("/getCityById/{Stateid}", GetCityById)
                .RequireAuthorization("GetCityById")
                .WithName("getCityById")
                .WithOpenApi();

            return builder;
        }
        private static async Task<IResult> AddCityAsync([FromServices] ICityServices cityServices, [FromBody] AddCityDto addCityDto)
        {
            if (cityServices == null || string.IsNullOrWhiteSpace(addCityDto.CityName))
            {
                return Results.BadRequest(new
                {
                    Message = "Invalid input data."
                });
            }
            var result = await cityServices.AddCityAsync(addCityDto);
            return Results.Ok(new
            {
                Message = result
            });
        }
        private static async Task<List<CityDto>> GetAllCityAsync([FromServices] ICityServices cityServices)
        {
            return await cityServices.GetAllCityAsync();
        }
        private static async Task<IResult> DeleteCityAsync([FromServices] ICityServices cityServices, Guid id)
        {
            var result = await cityServices.DeleteCityAsync(id);
            return Results.Ok(new
            {
                Message = result
            });
        }
        private static async Task<IResult> GetCityById([FromServices] ICityServices cityServices, Guid Stateid)
        {
            var result = await cityServices.GetCityByIdAsync(Stateid);
            return Results.Ok(new
            {
                Message = result
            });
        }
    }
}
