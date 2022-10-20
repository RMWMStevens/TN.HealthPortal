using TN.HealthPortal.Lib.Enums;

namespace TN.HealthPortal.Lib.Models
{
    public abstract class Scheme
    {
        public string Dose { get; set; }

        public string Timing { get; set; }

        public ProductionPhase ProductionPhase { get; set; }

        public string PigCategory { get; set; }

        public Product Product { get; set; }
    }
}
