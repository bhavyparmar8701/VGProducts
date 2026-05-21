using VGProducts.Entities.DTOs;

namespace VGProducts.Repository.Interface
{
    public interface IAuthRepository
    {
        Task<(bool Success, string Message)> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
        Task<GetRegisterUserDto> GetUserById(Guid id);
        Task<string> UpdateUserAsync(Guid userId, UpdateRegisterUserDto updateRegisterUserDto);
        Task<List<GetAllRegisterUserDto>> GetAllUser();
        Task<string> ChangePasswordAsync(Guid userId, ChangePasswordDto dto);
        Task<string> ForgotPasswordAsync(ForgetPasswordDto forgetPasswordDto);
        Task<string> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);

        //Task<string> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        //Task<string> RequestResetAsync(ForgetPasswordDto requestResetDto);
        //Task<string> VerifyOtpAsync(VerifyOtpDto verifyOtpDto);
    }
}
