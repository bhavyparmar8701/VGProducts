using Microsoft.AspNetCore.Mvc;
using VGProducts.Service.Interface;

namespace VGProducts.API.Extentions
{
    public static class InvoiceExtentions
    {
        public static RouteGroupBuilder MapInvoiceRoute(this RouteGroupBuilder builder)
        {
            builder.MapGet("/Invoice/{orderId}/{userId}", GetInvoiceAsync)
                    .RequireAuthorization("GetInvoice")
                    .WithName("getInvoice")
                    .WithOpenApi();

            return builder;
        }

        private static async Task<IResult> GetInvoiceAsync(
            [FromServices] IInvoiceServices invoiceServices,
            Guid orderId,
            Guid userId)
        {
            var result = await invoiceServices.GenerateInvoiceAsync(orderId, userId);

            return Results.File(
                result,
                "application/pdf",
                $"Invoice_{orderId}.pdf",
                enableRangeProcessing: false
            );
        }
    }
}
