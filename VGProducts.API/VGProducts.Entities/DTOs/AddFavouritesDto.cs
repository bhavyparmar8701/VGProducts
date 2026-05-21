using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public  class AddFavouritesDto
    {
        public Guid UserId { get; set; }
        public required Guid ProductId { get; set; }

    }
}
