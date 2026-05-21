using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGProducts.Entities.DTOs
{
    public class TokenResponseDto
    {
        public string Accesstoken { get; set; }
        public string RefreshToken { get; set; }
    }
}
