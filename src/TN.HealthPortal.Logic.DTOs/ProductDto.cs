namespace TN.HealthPortal.Logic.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }

        public ManufacturerDto Manufacturer { get; set; } = new();
    }
}