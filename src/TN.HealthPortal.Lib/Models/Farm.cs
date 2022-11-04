namespace TN.HealthPortal.Lib.Models
{
    public class Farm
    {
        public string Name { get; set; }

        public string BlnNumber { get; set; }

        public string PremiseID { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string SiteType { get; set; }

        public int Capacity { get; set; }

        public Source Source { get; set; }

        public List<DiseaseStatus> DiseaseStatuses { get; set; } = new();

        public List<DewormingScheme> DewormingSchemes { get; set; } = new();

        public List<VaccinationScheme> VaccinationSchemes { get; set; } = new();

        public List<Veterinarian> Veterinarians { get; set; } = new();
    }
}
