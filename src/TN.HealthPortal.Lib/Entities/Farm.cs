using TN.HealthPortal.Lib.Entities.Common;
using TN.HealthPortal.Lib.Enums;

namespace TN.HealthPortal.Lib.Entities
{
    public class Farm : Entity
    {
        public string BlnNumber { get; set; }

        public string Name { get; set; }

        public string PremiseId { get; set; }

        public string Description { get; set; }

        public Address Address { get; set; }

        public ICollection<ProductionType> ProductionTypes { get; set; }

        public int Capacity { get; set; }

        public string History { get; set; }

        public ICollection<Source> Sources { get; set; }

        public ICollection<DiseaseStatus> DiseaseStatuses { get; set; }

        public ICollection<DewormingScheme> DewormingSchemes { get; set; }

        public ICollection<VaccinationScheme> VaccinationSchemes { get; set; }

        public ICollection<Veterinarian> Veterinarians { get; set; }
    }
}
