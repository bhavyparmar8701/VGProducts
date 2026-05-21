using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class AddAddressDto 
    {
        public Guid UserId { get; set; }
        public string LandMark { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public Guid CityId { get; set; }
        public Guid StateId { get; set; }
        public Guid CountryId { get; set; }
        public int Pincode { get; set; }
        public string SaveAs { get; set; }
        
    }
}
