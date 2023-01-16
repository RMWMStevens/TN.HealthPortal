namespace TN.HealthPortal.Logic.DTOs.DropdownOptions
{
    public class SchemeDropdownOptionsDto
    {
        public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();

        public IEnumerable<ManufacturerDto> Manufacturers { get; set; } = new List<ManufacturerDto>();
    }
}
