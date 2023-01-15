namespace TN.HealthPortal.Logic.DTOs
{
    public class VeterinarianDto
    {
        public string EmployeeCode { get; set; }

        public string Name { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<CountryDto> Countries { get; set; } = new List<CountryDto>();

        public ICollection<RegionDto> Regions { get; set; } = new List<RegionDto>();
    }
}