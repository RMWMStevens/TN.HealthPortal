using System.ComponentModel.DataAnnotations.Schema;

namespace TN.HealthPortal.Data.EF.Entities
{
    [Table("Farms")]
    public class FarmEntity
    {
        public string Name { get; set; }

        public string BlnNumber { get; set; }

        public string PremiseID { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string SiteType { get; set; }

        public int Capacity { get; set; }

        public SourceEntity Source { get; set; }

        public List<DiseaseStatusEntity> DiseaseStatuses { get; set; }

        public List<DewormingSchemeEntity> DewormingSchemes { get; set; }

        public List<VaccinationSchemeEntity> VaccinationSchemes { get; set; }

        public List<VeterinarianEntity> Veterinarians { get; set; }
    }
}
