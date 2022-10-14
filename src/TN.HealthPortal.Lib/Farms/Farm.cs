using TN.HealthPortal.Lib.Users;

namespace TN.HealthPortal.Lib.Farms
{
    public class Farm
    {
        public string Name { get; set; }

        public string BlnNumber { get; set; }

        public string PremiseID { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string SiteType { get; set; }

        public int Capacity { get; set; }

        public Source Source { get; set; }

        public HealthStatus HealthStatuses { get; set; }

        public DewormingProtocol? DewormingProtocol { get; set; }

        public VaccinationProtocol? VaccinationProtocol { get; set; }

        public List<Veterinarian> Veterinarians { get; set; }
    }
}
