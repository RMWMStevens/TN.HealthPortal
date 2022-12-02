using TN.HealthPortal.Logic.Entities.Common;

namespace TN.HealthPortal.Logic.Entities
{
    public class Pathogen : Entity, IReusableEntity
    {
        public string Name { get; set; }
    }
}
