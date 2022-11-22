using TN.HealthPortal.Logic.Entities.Common;

namespace TN.HealthPortal.Logic.Entities
{
    public class DiseaseStatus : Entity
    {
        public string FarmBlnNumber { get; set; }

        public string Disease { get; set; }

        public string Status { get; set; }
    }
}
