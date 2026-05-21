using Microsoft.AspNetCore.Mvc;
using QRCoder;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Service.Interface;

namespace VGProducts.API.Extentions
{
    public static class OrderExtention
    {
        public static RouteGroupBuilder MapOrderRoute(this RouteGroupBuilder builder)
        {
            builder.MapPost("/addOrder", CreateOrderAsync)
                   .RequireAuthorization("CreateOrder")
                   .WithName("addOrder")
                   .WithOpenApi();

            builder.MapGet("/GetAllOrder", GetAllOrderAsync)
                    .RequireAuthorization("GetOrder")
                    .WithName("getAllOrder");

            builder.MapDelete("/DeleteOrder/{orderId}", DeleteOrderAsync)
                    .RequireAuthorization("DeleteOrder")
                    .WithName("deleteOrder")
                    .WithOpenApi();

            builder.MapPut("/SelectPaymentMethod/{orderId}", SelectPaymentMethod)
                    .RequireAuthorization("SelectPaymentMethod")
                    .WithName("selectPaymentMethod")
                    .WithOpenApi();

            builder.MapGet("/GetPaymentQr/PaymentQr", GetPaymentQr)
                    .RequireAuthorization("GetPaymentQr")
                    .WithName("getPaymentQr")
                    .WithOpenApi();

            return builder;
        }
        private static async Task<IResult> CreateOrderAsync([FromServices] IOrderServices orderServices, [FromBody] CreateOrderDto dto)
        {
            
            var result = await orderServices.CreateOrderAsync(dto);

            return Results.Ok(result);
        }

        private static async Task<IResult> GetAllOrderAsync( [FromServices] IOrderServices orderServices,Guid userId)
        {
            
            var result = await orderServices.GetOrdersAsync(userId);
            return Results.Ok(result);
        }
        private static async Task<IResult> DeleteOrderAsync( [FromServices] IOrderServices orderServices, Guid orderId, Guid userId)
        {
         
                var result = await orderServices.DeleteOrderAsync(orderId, userId);
                return Results.Ok(result);
            
        }
        private static async Task<IResult> SelectPaymentMethod([FromServices] IOrderServices orderServices, Guid orderId, PaymentMethod method, Guid userId)
        {
            
            var result = await orderServices.SelectPaymentMethod(orderId, method, userId);
            return Results.Ok(result);
        }
        private static async Task<IResult> GetPaymentQr([FromServices] IOrderServices orderServices, Guid userId)
        {
           

            var qrBytes = await orderServices.GetPaymentQr(userId);

            return Results.File(qrBytes, "image/png");
        }
    }
}
