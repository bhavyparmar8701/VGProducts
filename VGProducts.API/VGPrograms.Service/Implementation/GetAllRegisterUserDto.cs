using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Base;

namespace VGProducts.Service.Implementation
{
    public class GetAllRegisterUserDto : BaseDataEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfimed { get; set; }
    }
}
