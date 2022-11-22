namespace TN.HealthPortal.Logic.Entities.Common
{
    public abstract class Entity
    {
        public DateTime CreatedAt { get; private set; }

        public string CreatedByEmployeeCode { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public string UpdatedByEmployeeCode { get; private set; }

        public void SetCreatedAndUpdated(Veterinarian veterinarian)
        {
            if (CreatedByEmployeeCode != null)
                throw new InvalidOperationException("'Created' was already set, and cannot be set twice");

            CreatedByEmployeeCode = UpdatedByEmployeeCode = veterinarian.EmployeeCode;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        public void SetUpdated(Veterinarian veterinarian)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedByEmployeeCode = veterinarian.EmployeeCode;
        }
    }
}
