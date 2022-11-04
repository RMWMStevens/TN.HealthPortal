using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TN.HealthPortal.Data.EF.Entities
{
    [Table("DiseaseStatuses")]
    public class DiseaseStatusEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid Id { get; set; }

        public string Disease { get; set; }

        public string Status { get; set; }
    }
}
