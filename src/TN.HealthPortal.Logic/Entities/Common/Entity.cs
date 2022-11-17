namespace TN.HealthPortal.Logic.Entities.Common
{
    public abstract class Entity
    {
        public DateTime CreatedAt { get; private set; }

        public Veterinarian CreatedBy { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public Veterinarian UpdatedBy { get; private set; }

        public void SetCreatedAndUpdated(Veterinarian veterinarian)
        {
            if (CreatedBy != null)
                throw new InvalidOperationException("'Created' was already set, and cannot be set twice");

            CreatedBy = UpdatedBy = veterinarian;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        public void SetUpdated(Veterinarian veterinarian)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = veterinarian;
        }
    }
}
