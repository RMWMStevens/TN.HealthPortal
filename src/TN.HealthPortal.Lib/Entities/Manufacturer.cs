namespace TN.HealthPortal.Lib.Entities
{
    public class Manufacturer
    {
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
