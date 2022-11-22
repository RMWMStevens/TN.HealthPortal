namespace TN.HealthPortal.Logic.Entities
{
    public class CountryVeterinarian
    {
        public Country Country { get; set; }

        public ICollection<Veterinarian> Veterinarians { get; set; }
    }
}