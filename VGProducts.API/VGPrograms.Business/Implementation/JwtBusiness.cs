using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Repository.Interface;

namespace VGProducts.Business.Implementation
{
    public class JwtBusiness : IJwtBusiness
    {
        private readonly IJwtRepository _jwtRepository;

        public JwtBusiness(IJwtRepository jwtRepository)
        {
            _jwtRepository = jwtRepository;
        }

        public async Task<string> GenerateToken(ApplicationUser applicationUser)
        {
            return await _jwtRepository.GenerateToken(applicationUser);
        }

        string IJwtBusiness.GenerateRefreshToken()
        {
            return _jwtRepository.GenerateRefreshToken();
        }
    }
}
