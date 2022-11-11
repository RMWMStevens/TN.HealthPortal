using TN.HealthPortal.Lib.Entities;

namespace TN.HealthPortal.Lib.Services
{
    public interface IFarmService
    {
        Task AddAsync(Farm farm);

        Task<Farm> GetByBlnNumberAsync(string blnNumber);

        Task DeleteByBlnNumberAsync(string blnNumber);
    }
}
