using VGProducts.Entities.Base;
using VGProducts.Entities.DTOs;

namespace VGProducts.Entities.Model

{
    public class Review : BaseDataEntity
    {
        public Guid ReviewId { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }

        public decimal Rating { get; set; } 
        public string Comment { get; set; }

        public Product Product { get; set; }
        public  ApplicationUser User { get; set; }

    }
}
