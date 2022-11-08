using TN.HealthPortal.Lib.Entities.Common;

namespace TN.HealthPortal.Lib.Entities
{
    public class DiseaseStatus : Entity
    {
        public Guid Id { get; set; }

        public string Disease { get; set; }

        public string Status { get; set; }
    }
}
