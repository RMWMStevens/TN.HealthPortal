namespace TN.HealthPortal.Logic.DTOs
{
    public class CountryDto
    {
        public string Name { get; set; }

        public RegionDto Region { get; set; } = new();
    }
}