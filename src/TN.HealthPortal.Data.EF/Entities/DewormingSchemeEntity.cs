using System.ComponentModel.DataAnnotations.Schema;

namespace TN.HealthPortal.Data.EF.Entities
{
    [Table("DewormingSchemes")]
    public class DewormingSchemeEntity : SchemeEntity
    {
        public string RouteOfAdministration { get; set; }
    }
}
