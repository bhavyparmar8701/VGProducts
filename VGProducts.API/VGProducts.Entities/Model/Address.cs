using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Base;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.Model
{
    public class Address: BaseDataEntity
    {
        public Guid AddressId { get; set; }
        public Guid UserId { get; set; }
        public required string LandMark { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public Guid CityId { get; set; }
        public Guid StateId { get; set; }
        public Guid CountryId { get; set; }



        public int Pincode { get; set; }
        public string SaveAs { get; set; }
        public IsActive IsActive { get; set; }


        public City City { get; set; }
        public State State { get; set; }
        public Country Country { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
