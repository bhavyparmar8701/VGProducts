using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class SubCategory : BaseDataEntity
    {
        public Guid SubCategoryId { get; set; }
        public required string SubCategoryName { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public required Guid CategoryId { get; set; }
        public IsActive IsActive { get; set; }

        // Navigation
        public Category? Category { get; set; }

        public virtual ICollection<Product> Product { get; set; } = new List<Product>();

    }
}