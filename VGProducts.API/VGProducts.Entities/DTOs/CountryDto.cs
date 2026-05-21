using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;

namespace VGProducts.Entities.DTOs
{
    public class CountryDto : BaseDataEntity
    {
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public string IsActive { get; set; }
    }
}
