using TN.HealthPortal.Logic.DTOs.Enums;

namespace TN.HealthPortal.Logic.DTOs
{
    public class SchemeDto
    {
        public string FarmBlnNumber { get; set; }

        public string Dose { get; set; }

        public string Timing { get; set; }

        public ProductionPhase ProductionPhase { get; set; }

        public string PigCategory { get; set; }

        public ProductDto Product { get; set; } = new();

        public RouteOfAdministration RouteOfAdministration { get; set; }
    }
}