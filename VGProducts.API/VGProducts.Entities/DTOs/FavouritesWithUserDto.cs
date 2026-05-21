using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGProducts.Entities.DTOs
{
    public class FavouritesWithUserDto
    {
        public Guid UserId { get; set; }
        public List<FavouritesDto> Favourites { get; set; }
    }
}
