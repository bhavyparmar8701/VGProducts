using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class AddUserDto
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string FirstName { get; set; } 
        public required string LastName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
