namespace TN.HealthPortal.API.Models
{
    public class FarmCreationRequest
    {
        public string Name { get; set; }

        public string BlnNumber { get; set; }

        public string PremiseID { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string SiteType { get; set; }

        public int Capacity { get; set; }

        public string History { get; set; }
    }
}
