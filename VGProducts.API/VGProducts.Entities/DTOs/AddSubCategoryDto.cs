using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class AddSubCategoryDto
    {
        public required string SubCategoryName { get; set; }

        public Guid CategoryId { get; set; }

        public string? Description { get; set; }
        public IFormFile ImageUrl { get; set; }
    }
}
