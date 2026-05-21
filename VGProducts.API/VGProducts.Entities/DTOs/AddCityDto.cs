using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGProducts.Entities.DTOs
{
    public class AddCityDto
    {
        public required string CityName { get; set; }
        public required Guid StateId { get; set; }
    }
}
