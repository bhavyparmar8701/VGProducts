namespace VGProducts.Entities.DTOs
{
    public class AddStateDto
    {
        public required string StateName { get; set; }
        public Guid CountryId { get; set; }

    }
}
