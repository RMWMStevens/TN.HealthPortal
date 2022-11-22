using TN.HealthPortal.Logic.Entities.Common;

namespace TN.HealthPortal.Logic.Entities
{
    public class Veterinarian : Entity
    {
        public string EmployeeCode { get; set; }

        public string Name { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Farm> Farms { get; set; }

        public ICollection<Country> Countries { get; set; }

        public ICollection<Region> Regions { get; set; }
    }
}
