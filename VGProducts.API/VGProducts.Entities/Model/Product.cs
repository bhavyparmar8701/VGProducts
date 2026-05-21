using VGProducts.Entities.Base;
using VGProducts.Entities.Enums;
using VGProducts.Entities.Model;

namespace VGProducts.Entities.DTOs
{
    public class Product : BaseDataEntity
    {
        public Guid ProductId { get; set; }
        public required string ProductName { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public required decimal Price { get; set; }
        public required int Stock {  get; set; }
        public required string SKU { get; set; }
        public string? ImageUrl { get; set; }
        public required Guid SubCategoryId { get; set; }
        public IsActive IsActive { get; set; }
        public decimal? Reting { get; set; }
        public int? ReviewCount { get; set; }


        // Navigation properties
        public SubCategory? SubCategory { get; set; }

        
        public virtual ICollection<OrderItem> OrderItem { get; set; } = new List<OrderItem>();
        public virtual ICollection<CartItems> CartItems { get; set; } = new List<CartItems>();
        public virtual ICollection<Favourites> Favourites { get; set; } = new List<Favourites>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
