using TN.HealthPortal.Lib.Entities.Common;

namespace TN.HealthPortal.Lib.Entities
{
    public abstract class User : Entity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string EmployeeCode { get; set; }
    }
}
