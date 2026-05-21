using System.ComponentModel.DataAnnotations;
using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class Category : BaseDataEntity
    {
        public Guid CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public IsActive IsActive { get; set; }

        // Navigation properties
        public virtual ICollection<SubCategory> SubCategory { get; set; } = new List<SubCategory>();
    }
}