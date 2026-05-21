using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.Service.Implementation
{
    public class JwtServices : IJwtServices
    {
        private readonly IJwtBusiness _jwtBusiness;

        public JwtServices(IJwtBusiness jwtBusiness)
        {
            _jwtBusiness = jwtBusiness;
        }

        public string GenerateRefreshToken()
        {
            return _jwtBusiness.GenerateRefreshToken();
        }

        public async Task<string> GenerateToken(ApplicationUser applicationUser)
        {
            return await _jwtBusiness.GenerateToken(applicationUser);
        }
    }
}
