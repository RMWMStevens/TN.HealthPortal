using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TN.HealthPortal.Data.EF.Entities
{
    [Table("Manufacturers")]
    public class ManufacturerEntity
    {
        [Key]
        public string Name { get; set; }

        public List<ProductEntity> Products { get; set; }
    }
}
