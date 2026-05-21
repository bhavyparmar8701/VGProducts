using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Repository.Interface;

namespace VGProducts.Business.Implementation
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IAuthRepository authRepository;

        public AuthBusiness(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        public async Task<string> ChangePasswordAsync(Guid userId, ChangePasswordDto dto)
        {
            return await authRepository.ChangePasswordAsync(userId, dto);
        }

        public async Task<string> ForgotPasswordAsync(ForgetPasswordDto forgetPasswordDto)
        {
            return await authRepository.ForgotPasswordAsync(forgetPasswordDto);
        }

        public async Task<List<GetAllRegisterUserDto>> GetAllUser()
        {
            return await authRepository.GetAllUser();
        }

        public async Task<GetRegisterUserDto> GetUserById(Guid userId)
        {
            return await authRepository.GetUserById(userId);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            return await authRepository.LoginAsync(loginDto);
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            return await authRepository.RefreshTokenAsync(refreshToken);
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterDto registerDto)
        {
            return await authRepository.RegisterAsync(registerDto);
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            return await authRepository.ResetPasswordAsync(resetPasswordDto);
        }

        public async Task<string> UpdateUserAsync(Guid userId, UpdateRegisterUserDto updateRegisterUserDto)
        {
            return await authRepository.UpdateUserAsync(userId, updateRegisterUserDto);
        }
    }
}
