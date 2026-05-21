using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.Model
{
    public class City : BaseDataEntity 
    {
        public Guid CityId { get; set; }
        public required string CityName { get; set; } 
        public Guid StateId { get; set; }
        public IsActive IsActive { get; set; }
        public State State { get; set; }
        public virtual ICollection<Address> Address { get; set; } = new List<Address>();
    }
}
