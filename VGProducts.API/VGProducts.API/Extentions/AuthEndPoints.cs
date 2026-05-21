using Microsoft.AspNetCore.Mvc;
using MiniValidation;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.API.Extentions
{
    public static class AuthEndPoints
    {
        public static RouteGroupBuilder MapUserRoute(this RouteGroupBuilder builder)
        {
            builder.MapPost("/register", Register)
                   .WithName("register")
                   .WithOpenApi();

            builder.MapPost("/login", LoginUser)
                .WithName("login")
                .WithOpenApi();

            builder.MapGet("/GetuserById/{userId}", GetUserById)
                   .WithName("GetuserById")
                   .WithOpenApi();

            builder.MapPut("/UpdateUser/{userId}", UpdateUserAsync)
                    .RequireAuthorization("UpdateUser")
                    .WithName("UpdateUser")
                    .WithOpenApi();

            builder.MapPost("/refresh", RefreshToken)
                    .WithName("refresh")
                    .WithOpenApi();

            builder.MapGet("/GetAllUser", GetAllUser)
                    .RequireAuthorization("GetAllUser")
                    .WithName("GetAllUser")
                    .WithOpenApi();

            builder.MapPost("/ChangePassword/{userId}", ChangePasswordAsync)
                    .RequireAuthorization("ChangePassword")
                    .WithName("ChangePassword")
                    .WithOpenApi();

            builder.MapPost("/ForgotPassword", ForgotPasswordAsync)
                    .WithName("ForgotPassword")
                    .WithOpenApi();
            
            builder.MapPost("/ResetPassword", ResetPasswordAsync)
                    .WithName("ResetPassword")
                    .WithOpenApi();






            //builder.MapPost("/RequestReset", RequestResetAsync)
            //        .WithName("RequestReset")
            //        .WithOpenApi();
            //builder.MapPost("/VerifyOtp", VerifyOtpAsync)
            //        .WithName("VerifyOtp")
            //        .WithOpenApi();
            //builder.MapPost("/ResetPassword", ResetPasswordAsync)
            //        .WithName("ResetPassword")
            //        .WithOpenApi();


            return builder;
        }



        private static async Task<IResult> Register([FromServices] IAuthServices authServices, RegisterDto registerDto)
        {
            if (registerDto == null)
                return Results.BadRequest("Invalid request body");

            var result = await authServices.RegisterAsync(registerDto);


            if (result.Success)
            {
                return Results.Ok(new { message = result.Message });
            }
            else
            {
                return Results.BadRequest(new { message = result.Message });
            }
        }


        private static async Task<IResult> LoginUser(HttpContext httpContext, IAuthServices authServices, LoginDto loginDto)
        {
            if (!MiniValidator.TryValidate(loginDto, out var errors))
            {
                return Results.BadRequest(new
                {
                    success = false,
                    message = "Validation failed",
                    errors
                });
            }

            var result = await authServices.LoginAsync(loginDto);

            if (!result.Success)
                return Results.BadRequest(result);

           
            httpContext.Session.SetString("JWT", result.AccessToken);

            
            httpContext.Response.Cookies.Append("JWT", result.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            });

            return Results.Ok(result);
        }
        private static async Task<IResult> GetUserById(Guid UserId, [FromServices] IAuthServices authServices)
        {
            var result = await authServices.GetUserById(UserId);
            return Results.Ok(result);
        }

        private static async Task<IResult> UpdateUserAsync(Guid userId, IAuthServices authServices, UpdateRegisterUserDto updateRegisterUserDto)
        {
            try
            {
                var result = await authServices.UpdateUserAsync(userId, updateRegisterUserDto);
                return Results.Ok(new
                {
                    message = "User Updated Successfully"
                });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }


        private static async Task<IResult> RefreshToken(IAuthServices authServices, RefreshTokenRequestDto refreshTokenDto, HttpContext httpContext)//, CancellationToken cancellationToken)
        {
            var refreshToken = httpContext.Request.Cookies["RefreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return Results.Unauthorized();

            var result = await authServices.RefreshTokenAsync(refreshTokenDto.RefreshToken);

            if (result == null)
                return Results.Unauthorized();

            httpContext.Response.Cookies.Append("JWT", result.AccessToken);
            httpContext.Response.Cookies.Append("RefreshToken", result.RefreshToken);

            return Results.Ok(result);
        }

        private static async Task<IResult> GetAllUser([FromServices] IAuthServices authServices)
        {
            var result = await authServices.GetAllUser();
            return Results.Ok(result);
        }
        private static async Task<IResult> ChangePasswordAsync(Guid userId, IAuthServices authServices, ChangePasswordDto changePasswordDto)
        {
            
            try
            {
                var result = await authServices.ChangePasswordAsync(userId, changePasswordDto);

                if (result != "Password Changed Successfully")
                {
                    return Results.BadRequest(result);
                }

                return Results.Ok(new { message = result }); 
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
        private static async Task<IResult> ForgotPasswordAsync(IAuthServices authServices, ForgetPasswordDto forgetPasswordDto)
        {
            var result = await authServices.ForgotPasswordAsync(forgetPasswordDto);
            
            return Results.Ok(result);
            
        }
        private static async Task<IResult> ResetPasswordAsync(IAuthServices authServices, ResetPasswordDto resetPasswordDto)
        {
            var result = await authServices.ResetPasswordAsync(resetPasswordDto);
            if (result.Contains("Password reset successfully"))
            {
                return Results.Ok(result);
            }
            else
            {
                return Results.BadRequest(result);
            }
        }


        //private static async Task<IResult> ResetPasswordAsync(IAuthServices authServices, ResetPasswordDto resetPasswordDto)
        //{
        //    var result = await authServices.ResetPasswordAsync(resetPasswordDto);
        //    if (result.Contains("Invalid") || result.Contains("Expired"))
        //        return Results.BadRequest(result);
        //    return Results.Ok(result);
        //}
        //private static async Task<IResult> RequestResetAsync(IAuthServices authServices, ForgetPasswordDto requestResetDto)
        //{
        //    var result = await authServices.RequestResetAsync(requestResetDto);
        //    if (result.Contains("Invalid"))
        //        return Results.BadRequest(result);
        //    return Results.Ok(result);
        //}

        //private static async Task<IResult> VerifyOtpAsync(IAuthServices authServices, VerifyOtpDto verifyOtpDto)
        //{
        //    var result = await authServices.VerifyOtpAsync(verifyOtpDto);
        //    if (result.Contains("Invalid") || result.Contains("Expired"))
        //        return Results.BadRequest(result);
        //    return Results.Ok(result);
        //}

    }
}
