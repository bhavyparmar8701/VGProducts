using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class FavouritesDto
    {
        public Guid FavouritesId { get; set; }
        public required Guid UserId { get; set; }
        public required Guid ProductId { get; set; }
        public string IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
