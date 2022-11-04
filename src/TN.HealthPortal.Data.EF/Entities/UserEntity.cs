using System.ComponentModel.DataAnnotations;

namespace TN.HealthPortal.Data.EF.Entities
{
    public abstract class UserEntity
    {
        [Key]
        public string EmployeeCode { get; set; }

        public string Name { get; set; }
    }
}
