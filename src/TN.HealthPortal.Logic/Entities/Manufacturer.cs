using TN.HealthPortal.Logic.Entities.Common;

namespace TN.HealthPortal.Logic.Entities
{
    public class Manufacturer : IReusableEntity
    {
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
