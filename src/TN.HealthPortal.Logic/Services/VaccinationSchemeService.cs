using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Entities.Common;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Logic.Services
{
    public class VaccinationSchemeService : IVaccinationSchemeService
    {
        readonly IVaccinationSchemeRepository vaccinationSchemeRepository;

        public VaccinationSchemeService(IVaccinationSchemeRepository vaccinationSchemeRepository)
        {
            this.vaccinationSchemeRepository = vaccinationSchemeRepository;
        }

        public async Task AddAsync(VaccinationScheme vaccinationScheme)
        {
            await vaccinationSchemeRepository.AddAsync(vaccinationScheme);
        }

        public Task<IEnumerable<VaccinationScheme>> GetByBlnNumberAsync(string blnNumber)
                => vaccinationSchemeRepository.FindAsync(_ => _.FarmBlnNumber == blnNumber);
    }
}
