namespace TN.HealthPortal.Lib.Models
{
    public class Veterinarian : User
    {
        public List<Farm> Farms { get; set; } = new();
    }
}
