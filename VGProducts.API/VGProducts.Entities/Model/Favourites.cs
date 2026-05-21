
using VGProducts.Entities.Base;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.Model
{
    public class Favourites : BaseDataEntity
    {
        public Guid FavouritesId { get; set; }
        public required Guid UserId { get; set; }
        public required Guid ProductId { get; set; }
        public IsActive IsActive { get; set; }

        // Navigation properties
        public ApplicationUser ApplicationUser { get; set; }
        public Product Product { get; set; }
    }
}
