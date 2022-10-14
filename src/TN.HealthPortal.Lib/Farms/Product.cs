namespace TN.HealthPortal.Lib.Farms
{
    public abstract class Product
    {
        public string Name { get; set; }

        public Manufacturer Manufacturer { get; set; }
    }
}
