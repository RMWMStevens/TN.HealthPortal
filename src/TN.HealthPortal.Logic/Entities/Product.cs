using TN.HealthPortal.Logic.Entities.Common;

namespace TN.HealthPortal.Logic.Entities
{
    public class Product : Entity, IReusableEntity
    {
        public string Name { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public bool IsApproved { get; set; }
    }
}
