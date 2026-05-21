using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.Service.Implementation
{
    public class AuthServices : IAuthServices
    {
        private readonly IAuthBusiness authBusiness;

        public AuthServices(IAuthBusiness authBusiness)
        {
            this.authBusiness = authBusiness;
        }

        public async Task<string> ChangePasswordAsync(Guid userId, ChangePasswordDto dto)
        {
            return await authBusiness.ChangePasswordAsync(userId, dto);
        }

        public async Task<string> ForgotPasswordAsync(ForgetPasswordDto forgetPasswordDto)
        {
            return await authBusiness.ForgotPasswordAsync(forgetPasswordDto);
        }

        public async Task<List<Entities.DTOs.GetAllRegisterUserDto>> GetAllUser()
        {
            return await authBusiness.GetAllUser();
        }

        public async Task<GetRegisterUserDto> GetUserById(Guid userId)
        {
            return await authBusiness.GetUserById(userId);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            return await authBusiness.LoginAsync(loginDto);
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            return await authBusiness.RefreshTokenAsync(refreshToken);
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterDto registerDto)
        {
            return await authBusiness.RegisterAsync(registerDto);
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            return await authBusiness.ResetPasswordAsync(resetPasswordDto);
        }

        public async Task<string> UpdateUserAsync(Guid userId, UpdateRegisterUserDto updateRegisterUserDto)
        {
            return await authBusiness.UpdateUserAsync(userId,updateRegisterUserDto);
        }
    }
}
