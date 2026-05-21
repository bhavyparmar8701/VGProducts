using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Base;

namespace VGProducts.Entities.DTOs
{
    public class AddressWithUserDto : BaseDataEntity
    {
        public Guid AddressId { get; set; }
        public Guid UserId { get; set; }

        public string LandMark { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public Guid CityId { get; set; }
        public string CityName { get; set; }     

        public Guid StateId { get; set; }
        public string StateName { get; set; }    

        public Guid CountryId { get; set; }
        public string CountryName { get; set; }  

        public int Pincode { get; set; }
        public string SaveAs { get; set; }
        public string IsActive { get; set; }

        
    }
}
