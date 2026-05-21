using System.ComponentModel.DataAnnotations;
using VGProducts.Entities.Base;

namespace VGProducts.Entities.DTOs
{
    public class GetRegisterUserDto 
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
