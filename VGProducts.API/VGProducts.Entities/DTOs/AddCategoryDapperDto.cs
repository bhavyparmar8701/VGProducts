using System.ComponentModel.DataAnnotations;
using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class AddCategoryDapperDto
    {

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(20, ErrorMessage = "Max length is 20")]
        public required string CategoryName { get; set; }


        [StringLength(200, ErrorMessage = "Max length is 200")]
        public string? Description { get; set; }


        [StringLength(200, ErrorMessage = "Max length is 200")]
        public string? ImageUrl { get; set; }

    }
}
