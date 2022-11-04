using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TN.HealthPortal.Data.EF.Entities
{
    [Table("Sources")]
    public class SourceEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid Id { get; set; }

        public string Gilt { get; set; }

        public string Boar { get; set; }

        public string Semen { get; set; }

        public string History { get; set; }
    }
}
