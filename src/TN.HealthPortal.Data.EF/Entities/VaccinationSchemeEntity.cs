using System.ComponentModel.DataAnnotations.Schema;

namespace TN.HealthPortal.Data.EF.Entities
{
    [Table("VaccinationSchemes")]
    public class VaccinationSchemeEntity : SchemeEntity
    {
        public PathogenEntity Pathogen { get; set; }
    }
}
