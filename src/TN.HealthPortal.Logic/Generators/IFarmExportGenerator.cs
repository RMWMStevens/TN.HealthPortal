using TN.HealthPortal.Logic.Entities;

namespace TN.HealthPortal.Logic.Generators
{
    public interface IFarmExportGenerator
    {
        byte[] Generate(Farm farm);
    }
}