using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class UpdateSubCategoryDto 
    {
        public string SubCategoryName { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public IFormFile ImageUrl { get; set; }
        public IsActive IsActive { get; set; }
    }
}
