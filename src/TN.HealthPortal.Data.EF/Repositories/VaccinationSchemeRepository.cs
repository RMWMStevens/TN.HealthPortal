using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Data.EF.Repositories
{
    internal class VaccinationSchemeRepository : Repository<VaccinationScheme>, IVaccinationSchemeRepository
    {
        public VaccinationSchemeRepository(AppDbContext context) : base(context) { }
    }
}
