namespace VGProducts.Entities.Base
{
    public class BaseDataEntity : IBaseDataEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
