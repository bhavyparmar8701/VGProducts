using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class CityDto : BaseDataEntity
    {
        public Guid CityId { get; set; }
        public string CityName { get; set; }
        public Guid StateId { get; set; }
        public string IsActive { get; set; }
    }
}
