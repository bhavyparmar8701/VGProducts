using Microsoft.AspNetCore.Mvc;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.API.Extentions
{
    public static class StateExtentions
    {
        public static RouteGroupBuilder MapStateRoute(this RouteGroupBuilder builder)
        {
            builder.MapPost("/addstate", AddStateAsync)
                   .RequireAuthorization("CreateState")
                   .WithName("addstate")
                   .WithOpenApi();

            builder.MapGet("/getState", GetAllStateAsync)
                .RequireAuthorization("GetState")
                .WithName("getState")
                .WithOpenApi();

            builder.MapDelete("/deleteState/{id}", DeleteStateAsync)
                .RequireAuthorization("DeleteState")
                .WithName("deleteState")
                .WithOpenApi();

            builder.MapGet("/getStateById/{Countryid}", GetStateById)
                .RequireAuthorization("GetStateById")
                .WithName("getStateById")
                .WithOpenApi();

            return builder;
        }
        private static async Task<IResult> AddStateAsync([FromServices] IStateServices stateServices, [FromBody] AddStateDto addStateDto)
        {
            if (stateServices == null || string.IsNullOrWhiteSpace(addStateDto.StateName))
            {
                return Results.BadRequest(new
                {
                    Message = "Invalid input data."
                });
            }
            var result = await stateServices.AddStateAsync(addStateDto);
            return Results.Ok(new
            {
                Message = result
            });
        }
        private static async Task<List<StateDto>> GetAllStateAsync([FromServices] IStateServices stateServices)
        {
            return await stateServices.GetAllStateAsync();
        }
        private static async Task<IResult> DeleteStateAsync([FromServices] IStateServices stateServices, Guid id)
        {
            var result = await stateServices.DeleteStateAsync(id);
            return Results.Ok(new
            {
                Message = result
            });
        }
        private static async Task<IResult> GetStateById([FromServices] IStateServices stateServices, Guid Countryid)
        {
            var result = await stateServices.GetStateByIdAsync(Countryid);
            return Results.Ok(new
            {
                Message = result
            });
        }
    }
}
