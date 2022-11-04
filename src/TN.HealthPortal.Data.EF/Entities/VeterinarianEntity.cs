using System.ComponentModel.DataAnnotations.Schema;

namespace TN.HealthPortal.Data.EF.Entities
{
    [Table("Veterinarians")]
    public class VeterinarianEntity : UserEntity
    {
        public List<FarmEntity> Farms { get; set; }
    }
}
