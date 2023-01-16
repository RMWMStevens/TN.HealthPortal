using TN.HealthPortal.Logic.Entities;

namespace TN.HealthPortal.Logic.Services
{
    public interface IFarmService
    {
        Task AddAsync(Farm farm);

        Task<IEnumerable<Farm>> GetAllAsync(Veterinarian? veterinarian);

        Task<IEnumerable<Farm>> GetAllOutdatedAsync(Veterinarian? veterinarian);

        Task<Farm?> GetByBlnNumberAsync(string blnNumber);

        Task DeleteByBlnNumberAsync(string blnNumber);

        Task<byte[]> GeneratePdfHealthReportAsync(string blnNumber);
    }
}
