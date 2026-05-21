using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class StateDto : BaseDataEntity
    {
        public Guid StateId { get; set; }
        public string StateName { get; set; }
        public Guid CountryId { get; set; }
        public string IsActive { get; set; }
    }
}
