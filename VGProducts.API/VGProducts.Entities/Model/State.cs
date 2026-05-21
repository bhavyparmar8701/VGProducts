using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.Model
{
    public class State : BaseDataEntity
    {
        public Guid StateId { get; set; }
        public required string StateName { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public IsActive IsActive { get; set; }
        public virtual ICollection<City> City { get; set; } = new List<City>();
        public virtual ICollection<Address> Address { get; set; } = new List<Address>();
    }
}
