using TN.HealthPortal.Logic.Entities.Common;
using TN.HealthPortal.Logic.Enums;

namespace TN.HealthPortal.Logic.Entities
{
    public abstract class Scheme : Entity
    {
        public Guid Id { get; set; }

        public string Dose { get; set; }

        public string Timing { get; set; }

        public ProductionPhase ProductionPhase { get; set; }

        public string PigCategory { get; set; }

        public Product Product { get; set; }

        public RouteOfAdministration RouteOfAdministration { get; set; }
    }
}
