using VGProducts.Entities.Base;
using VGProducts.Entities.DTOs;

namespace VGProducts.Entities.Model
{
    public class Cart : BaseDataEntity
    {
        public Guid CartId { get; set; }
        public required Guid UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<CartItems> CartItems { get; set; } = new List<CartItems>();

    }
}
