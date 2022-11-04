using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TN.HealthPortal.Lib.Enums;

namespace TN.HealthPortal.Data.EF.Entities
{
    [Table("Schemes")]
    public abstract class SchemeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid Id { get; set; }

        public string Dose { get; set; }

        public string Timing { get; set; }

        public ProductionPhase ProductionPhase { get; set; }

        public string PigCategory { get; set; }

        public ProductEntity Product { get; set; }
    }
}
