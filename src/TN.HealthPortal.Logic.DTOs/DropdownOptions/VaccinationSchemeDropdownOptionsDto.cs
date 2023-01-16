namespace TN.HealthPortal.Logic.DTOs.DropdownOptions
{
    public class VaccinationSchemeDropdownOptionsDto : SchemeDropdownOptionsDto
    {
        public IEnumerable<PathogenDto> Pathogens { get; set; } = new List<PathogenDto>();
    }
}
