using TN.HealthPortal.Logic.Entities.Common;

namespace TN.HealthPortal.Logic.Entities
{
    public class Product : Entity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Manufacturer Manufacturer { get; set; }
    }
}
