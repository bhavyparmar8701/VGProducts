using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;

namespace VGProducts.Repository.Interface
{
    public interface IJwtRepository
    {
        Task<string> GenerateToken(ApplicationUser applicationUser);
        string GenerateRefreshToken();
    }
}
