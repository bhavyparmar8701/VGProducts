using System.ComponentModel.DataAnnotations;

namespace VGProducts.Entities.DTOs
{
    public class RegisterUserDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfimed { get; set; }
    }
}
