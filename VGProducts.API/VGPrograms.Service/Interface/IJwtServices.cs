using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;

namespace VGProducts.Service.Interface
{
    public interface IJwtServices
    {
        Task<string> GenerateToken(ApplicationUser applicationUser);
        string GenerateRefreshToken();
    }
}
