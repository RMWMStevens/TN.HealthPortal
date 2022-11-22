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

        public ICollection<ProductionTypeDto> ProductionTypes { get; set; } = new List<ProductionTypeDto>();

        public int Capacity { get; set; }

        public string History { get; set; }

        public ICollection<SourceDto> Sources { get; set; } = new List<SourceDto>();

        public ICollection<DiseaseStatusDto> DiseaseStatuses { get; set; } = new List<DiseaseStatusDto>();

    }
}
