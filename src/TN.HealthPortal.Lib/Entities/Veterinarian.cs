namespace TN.HealthPortal.Lib.Entities
{
    public class Veterinarian : User
    {
        public ICollection<Farm> Farms { get; set; }
    }
}
