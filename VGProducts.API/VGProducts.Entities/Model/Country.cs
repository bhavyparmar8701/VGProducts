using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.Model
{
    public class Country : BaseDataEntity
    {
        public Guid CountryId { get; set; }
        public required string CountryName { get; set; } 
        public IsActive IsActive { get; set; }
        public virtual ICollection<State> State { get; set; } = new List<State>();
        public virtual ICollection<Address> Address { get; set; } = new List<Address>();
    }
}
