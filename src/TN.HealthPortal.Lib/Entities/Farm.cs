using TN.HealthPortal.Lib.Entities.Common;

namespace TN.HealthPortal.Lib.Entities
{
    public class Farm : Entity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string BlnNumber { get; set; }

        public string PremiseID { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string SiteType { get; set; }

        public int Capacity { get; set; }

        public string History { get; set; }

        public Source Source { get; set; }

        public ICollection<DiseaseStatus> DiseaseStatuses { get; set; }

        public ICollection<DewormingScheme> DewormingSchemes { get; set; }

        public ICollection<VaccinationScheme> VaccinationSchemes { get; set; }

        public ICollection<Veterinarian> Veterinarians { get; set; }
    }
}
