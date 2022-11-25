using TN.HealthPortal.Logic.Entities;

namespace TN.HealthPortal.Logic.Services
{
    public interface IVaccinationSchemeService
    {
        Task AddAsync(VaccinationScheme vaccinationScheme);

        Task<IEnumerable<VaccinationScheme>> GetByBlnNumberAsync(string blnNumber);

    }
}
