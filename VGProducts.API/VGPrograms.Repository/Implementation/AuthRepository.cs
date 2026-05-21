using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VGProducts.Entities.DTOs;
using VGProducts.Repository.DataAccess;
using VGProducts.Repository.Interface;

namespace VGProducts.Repository.Implementation
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtRepository _jwtRepository;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEmailRepository _emailRepository;
        private readonly IConfiguration _configuration;

        public AuthRepository(UserManager<ApplicationUser> userManager, IJwtRepository jwtRepository, ApplicationDbContext applicationDbContext, IEmailRepository emailRepository, IConfiguration configuration)
        {
            _userManager = userManager;
            _jwtRepository = jwtRepository;
            _applicationDbContext = applicationDbContext;
            _emailRepository = emailRepository;
            _configuration = configuration;
        }
        public async Task<string> ChangePasswordAsync(Guid userId, ChangePasswordDto dto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return "User not found";
            }

            if (dto.NewPassword != dto.ConfirmPassword)
            {
                return "New Password and Confirm Password Do Not Match";
            }

            var isSame = await _userManager.CheckPasswordAsync(user, dto.NewPassword);
            if (isSame)
            {
                return "New Password Cannot Be Same As Old Password";
            }

            var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);

            if (!result.Succeeded)
            {
                return string.Join(", ", result.Errors.Select(e => e.Description));
            }

            user.UpdatedAt = DateTime.UtcNow;
            await _applicationDbContext.SaveChangesAsync();

            return "Password Changed Successfully";
        }

        public async Task<List<GetAllRegisterUserDto>> GetAllUser()
        {
            var user = await _applicationDbContext.Users.ToListAsync();
            return user.Select(u => new GetAllRegisterUserDto
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(u.PasswordHash),
                PhoneNumber = u.PhoneNumber,
                PhoneNumberConfimed = u.PhoneNumberConfirmed,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            }).ToList();
        }

        public async Task<GetRegisterUserDto> GetUserById(Guid userId)
        {
            var user = await _applicationDbContext.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            return new GetRegisterUserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid Email Or Password"
                };
            }


            if (await _userManager.IsLockedOutAsync(user))
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Account is locked. Try again later."
                };
            }

            
            var isValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isValid)
            {
                await _userManager.AccessFailedAsync(user);

                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password"
                };
            }

            var accessToken = await _jwtRepository.GenerateToken(user);
            var refreshToken = _jwtRepository.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(2);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            return new AuthResponseDto
            {
                Success = true,
                Message = "Login successful",
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null)
                return null;

            if (user.RefreshTokenExpiry <= DateTime.UtcNow)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiry = null;

                await _userManager.UpdateAsync(user);
                return null;
            }


            var newAccessToken = await _jwtRepository.GenerateToken(user);
            var newRefreshToken = _jwtRepository.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(2);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            return new AuthResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }



        public async Task<(bool Success, string Message)> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email,
                CreatedAt = DateTime.UtcNow
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                
                if (result.Errors.Any(e => e.Code == "DuplicateUserName" || e.Code == "DuplicateEmail"))
                {
                    return (false, "Already registered");
                }

                
                var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                return (false, errorMessage);
            }

            await _userManager.AddToRoleAsync(user, "User");

            return (true, "User Registered Successfully");
        }

        public async Task<string> UpdateUserAsync(Guid userId, UpdateRegisterUserDto updateRegisterUserDto)
        {
            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return "User Not Updated";

            user.FirstName = updateRegisterUserDto.FirstName;
            user.LastName = updateRegisterUserDto.LastName;
            user.PhoneNumber = updateRegisterUserDto.PhoneNumber;
            user.PhoneNumberConfirmed = true;
            user.UpdatedAt = DateTime.UtcNow;

            await _applicationDbContext.SaveChangesAsync();
            return "User Updated Successfully";
        }



        public async Task<string> ForgotPasswordAsync(ForgetPasswordDto forgetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgetPasswordDto.Email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var encodedToken = Uri.EscapeDataString(token);

                var baseUrl = _configuration["Frontend:BaseUrl"];

                var resetLink = $"{baseUrl}/reset-password?email={user.Email}&token={encodedToken}";

                var body = $@"
                        <h2>Password Reset</h2>
                        <p>Click the link below to reset your password:</p>

                        <p><a href='{resetLink}'>{resetLink}</a></p>

                        <p>This link will expire automatically.</p>
                        ";

                await _emailRepository.SendEmail(user.Email, "Reset Password", body);
                return "Password Reset Link Sent to Your Email";
            }
            else
            {
                return "User Not Found";
            }
        }



        public async Task<string> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user != null)
            {
                var decodedToken = Uri.UnescapeDataString(resetPasswordDto.Token);
                var result = await _userManager.ResetPasswordAsync(user, decodedToken, resetPasswordDto.NewPassword);

                if (!result.Succeeded)
                {
                    return string.Join(", ", result.Errors.Select(e => e.Description));
                }
                return "Password Reset Successfully";
            }
            else
            {
                return "User Not Found";
            }

        }



        //public async Task<string> RequestResetAsync(RequestResetDto requestResetDto)
        //{
        //    var user = await _userManager.FindByEmailAsync(requestResetDto.Email);
        //    if (user == null)
        //    {
        //        return "User Not Found";
        //    }
        //    var otp = new Random().Next(100000, 999999).ToString();
        //    user.ResetOtp = otp;
        //    user.OtpExpiry = DateTime.UtcNow.AddMinutes(10);
        //    user.IsOtpVerified = false;

        //    var result = await _userManager.UpdateAsync(user);

        //    if (!result.Succeeded)
        //    {
        //        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        //        return errors;
        //    }
        //    await _emailRepository.SendOtp(user.Email, otp);
        //    return "Otp Send Successfully";
        //}

        //public async Task<string> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        //{
        //    var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
        //    if (user == null)
        //    {
        //        return "User Not Found";
        //    }

        //    if (!user.IsOtpVerified)
        //    {
        //        return "OTP Expired";
        //    }

        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //    var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordDto.NewPassword);

        //    if (!result.Succeeded)
        //        return string.Join(", ", result.Errors.Select(e => e.Description));

        //    user.ResetOtp = null;
        //    user.OtpExpiry = null;
        //    user.IsOtpVerified = false;
        //    user.UpdatedAt = DateTime.UtcNow;

        //    var updateResult = await _userManager.UpdateAsync(user);

        //    if (!updateResult.Succeeded)
        //    {
        //        var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
        //        return errors;
        //    }
        //    return "Password Reset Successfully";

        //}



        //public async Task<string> VerifyOtpAsync(VerifyOtpDto verifyOtpDto)
        //{
        //    var user = await _userManager.FindByEmailAsync(verifyOtpDto.Email);
        //    if (user == null)
        //    {
        //        return "User Not Found";
        //    }


        //    if (string.IsNullOrEmpty(user.ResetOtp) || user.ResetOtp.Trim() != verifyOtpDto.Otp.Trim())
        //    {
        //        return "Invalid Otp";
        //    }

        //    if (user.OtpExpiry == null || user.OtpExpiry <= DateTime.UtcNow)
        //    {
        //        return "OTP Expired";
        //    }

        //    user.IsOtpVerified = true;
        //    user.ResetOtp = null;
        //    user.OtpExpiry = null;

        //    var result = await _userManager.UpdateAsync(user);

        //    if (!result.Succeeded)
        //    {
        //        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        //        return errors;
        //    }


        //    await _emailRepository.SendEmail(
        //            user.Email,
        //            "OTP Verified",
        //            "Your OTP has been successfully verified. You can now reset your password.");

        //    return "OTP Verified Successfully";
        //}
    }
}

