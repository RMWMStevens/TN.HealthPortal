using TN.HealthPortal.Lib.Farms;

namespace TN.HealthPortal.Lib.Users
{
    public class Veterinarian : User
    {
        public List<Farm> Farms { get; set; }
    }
}
