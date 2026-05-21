using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class CategoryDto : BaseDataEntity
    {
        public Guid CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string IsActive { get; set; }

    }
}
