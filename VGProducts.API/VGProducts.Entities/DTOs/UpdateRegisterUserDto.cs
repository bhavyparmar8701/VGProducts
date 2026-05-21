using System.ComponentModel.DataAnnotations;
using VGProducts.Entities.Base;

namespace VGProducts.Entities.DTOs
{
    public class UpdateRegisterUserDto 
    {
        public  string? FirstName { get; set; }
        public  string? LastName { get; set; }

        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter valid 10-digit Indian phone number")]
        public required string PhoneNumber { get; set; }
    }
}
