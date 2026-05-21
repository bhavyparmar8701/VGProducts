using Microsoft.AspNetCore.Http;
using VGProducts.Entities.Base;

namespace VGProducts.Entities.DTOs
{
    public class ProductDto : BaseDataEntity
    {
        public Guid ProductId { get; set; }
        public required string ProductName { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public required decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public required int Stock { get; set; }
        public required string SKU { get; set; }
        public string ImageUrl { get; set; }
        public required Guid SubCategoryId { get; set; }
        public string IsActive { get; set; }
        public decimal? Reting { get; set; }
        public int? ReviewCount { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }
}
