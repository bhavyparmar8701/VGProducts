using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Implementation;
using VGProducts.Service.Interface;

namespace VGProducts.API.Extentions
{
    public static class ReviewExtentions
    {
        public static RouteGroupBuilder MapReviewRoute(this RouteGroupBuilder builder)
        {
            builder.MapPost("/addorupdatereview", AddOrUpdateReviewAsync)
                   .RequireAuthorization("CreateOrUpdateReview")
                   .WithName("addorupdatereview")
                   .WithOpenApi();

            builder.MapGet("/getallproductreview", GetAllReviewAsync) 
                .WithName("getallproductreview")
                .WithOpenApi();
            return builder;
        }

        private static async Task<IResult> AddOrUpdateReviewAsync([FromServices] IReviewServices reviewServices, [FromBody] AddReviewDto addReviewDto)
        {
            var result = await reviewServices.AddOrUpdateReview(addReviewDto);
            return Results.Ok(result);
        }

        private static async Task<IResult> GetAllReviewAsync([FromServices] IReviewServices reviewServices)
        {
            var result = await reviewServices.GetAllReviewAsync();
            return TypedResults.Ok(result);
            
        }
    }
}
