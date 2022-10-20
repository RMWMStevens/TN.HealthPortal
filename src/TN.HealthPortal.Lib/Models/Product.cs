namespace TN.HealthPortal.Lib.Models
{
    public abstract class Product
    {
        public string Name { get; set; }

        public Manufacturer Manufacturer { get; set; }
    }
}
