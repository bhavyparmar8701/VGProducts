using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace VGProducts.Entities.DTOs
{
    public class AddProductDto
    {

        public required string ProductName { get; set; }
        public required string Description { get; set; }
        public required string ShortDescription { get; set; }
        public required decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public required int Stock { get; set; }
        public required string SKU { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public required Guid SubCategoryId { get; set; }
        public decimal? Rating { get; set; }
        public int ReviewCount { get; set; }

    }
}
