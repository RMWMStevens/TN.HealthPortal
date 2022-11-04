using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TN.HealthPortal.Data.EF.Entities
{
    [Table("Pathogens")]
    public class PathogenEntity
    {
        [Key]
        public string Name { get; set; }
    }
}
