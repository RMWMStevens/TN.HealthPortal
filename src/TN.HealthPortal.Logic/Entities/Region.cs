using TN.HealthPortal.Logic.Entities.Common;

namespace TN.HealthPortal.Logic.Entities
{
    public class Region : IReusableEntity
    {
        public string Name { get; set; }

        public ICollection<Veterinarian> Veterinarians { get; set; }
    }
}
