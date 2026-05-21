using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class CategoryDapperDto : BaseDataEntity
    {
        public Guid CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public IsActive IsActive { get; set; }

    }
}
