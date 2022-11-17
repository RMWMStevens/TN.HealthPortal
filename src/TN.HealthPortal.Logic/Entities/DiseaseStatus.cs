using TN.HealthPortal.Logic.Entities.Common;

namespace TN.HealthPortal.Logic.Entities
{
    public class DiseaseStatus : Entity
    {
        public Guid Id { get; set; }

        public string Disease { get; set; }

        public string Status { get; set; }
    }
}
