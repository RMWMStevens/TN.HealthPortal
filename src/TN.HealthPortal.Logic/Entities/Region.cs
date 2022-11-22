namespace TN.HealthPortal.Logic.Entities
{
    public class Region
    {
        public string Name { get; set; }

        public ICollection<Veterinarian> Veterinarians { get; set; }
    }
}
