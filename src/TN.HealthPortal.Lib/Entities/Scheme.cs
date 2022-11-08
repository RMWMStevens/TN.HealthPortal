using TN.HealthPortal.Lib.Entities.Common;
using TN.HealthPortal.Lib.Enums;

namespace TN.HealthPortal.Lib.Entities
{
    public abstract class Scheme : Entity
    {
        public Guid Id { get; set; }

        public string Dose { get; set; }

        public string Timing { get; set; }

        public ProductionPhase ProductionPhase { get; set; }

        public string PigCategory { get; set; }

        public Product Product { get; set; }
    }
}
