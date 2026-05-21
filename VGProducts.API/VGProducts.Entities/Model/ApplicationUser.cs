using Microsoft.AspNetCore.Identity;
using VGProducts.Entities.Base;
using VGProducts.Entities.Model;

namespace VGProducts.Entities.DTOs
{
    public class ApplicationUser : IdentityUser<Guid>, IBaseDataEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public string? ResetOtp { get; set; }
        public DateTime? OtpExpiry { get; set; }
        public bool IsOtpVerified { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }



        public virtual ICollection<Order> Order { get; set; } = new List<Order>();
        public virtual ICollection<Favourites> Favourites { get; set; } = new List<Favourites>();
        public virtual ICollection<Address> Address { get; set; } = new List<Address>();
        public virtual Cart Cart { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
