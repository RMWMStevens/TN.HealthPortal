using TN.HealthPortal.Logic.Entities.Common;

namespace TN.HealthPortal.Logic.Entities
{
    public class Country : IReusableEntity
    {
        public string Name { get; set; }

        public Region Region { get; set; }

        public ICollection<Veterinarian> Veterinarians { get; set; }
    }
}