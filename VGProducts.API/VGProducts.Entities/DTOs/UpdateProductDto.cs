using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class UpdateProductDto
    {
        public required string ProductName { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public required decimal Price { get; set; }
        public IFormFile ImageUrl { get; set; }
        public required int Stock { get; set; }
        public required string SKU { get; set; }
        public required Guid SubCategoryId { get; set; }
        public IsActive IsActive { get; set; }
    }
}
