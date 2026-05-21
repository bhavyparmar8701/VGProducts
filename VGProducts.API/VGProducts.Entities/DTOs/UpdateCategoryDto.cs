using System.ComponentModel.DataAnnotations;
using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class UpdateCategoryDto 
    {

        [StringLength(200, ErrorMessage = "Max length is 200")]
        public string? Description { get; set; }


        [StringLength(200, ErrorMessage = "Max length is 200")]
        public string? ImageUrl { get; set; }

        public IsActive IsActive { get; set; }
    }
}
