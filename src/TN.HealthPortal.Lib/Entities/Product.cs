using TN.HealthPortal.Lib.Entities.Common;

namespace TN.HealthPortal.Lib.Entities
{
    public class Product : Entity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Manufacturer Manufacturer { get; set; }
    }
}
