using TN.HealthPortal.Lib.Models;

namespace TN.HealthPortal.Lib.Repositories
{
    public interface ITempFarmRepository
    {
        void Add(Farm farm);
        Farm GetByBlnNumber(string blnNumber);
    }
}
