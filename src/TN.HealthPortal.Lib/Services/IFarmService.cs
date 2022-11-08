using TN.HealthPortal.Lib.Entities;

namespace TN.HealthPortal.Lib.Services
{
    public interface IFarmService
    {
        void Add(Farm farm);
        void AddFarmAsync(Farm farm);
        Farm GetByBlnNumber(string blnNumber);
    }
}
