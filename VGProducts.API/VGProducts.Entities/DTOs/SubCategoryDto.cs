using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class SubCategoryDto : BaseDataEntity
    {
        public Guid SubCategoryId { get; set; }
        public required string SubCategoryName { get; set; }

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public string IsActive { get; set; }

        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
