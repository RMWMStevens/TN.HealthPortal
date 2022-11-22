namespace TN.HealthPortal.Logic.DTOs
{
    public class FarmDto
    {
        public string Name { get; set; }

        public string BlnNumber { get; set; }

        public string PremiseID { get; set; }

        public string Description { get; set; }

        public AddressDto Address { get; set; }

        public CountryDto Country { get; set; }

        public ICollection<ProductionTypeDto> ProductionTypes { get; set; }

        public int Capacity { get; set; }

        public string History { get; set; }

        public ICollection<SourceDto> Sources { get; set; }

        public ICollection<DiseaseStatusDto> DiseaseStatuses { get; set; }

        public ICollection<DewormingSchemeDto> DewormingSchemes { get; set; }

        public ICollection<VaccinationSchemeDto> VaccinationSchemes { get; set; }

        public ICollection<VeterinarianDto> Veterinarians { get; set; }
    }
}
