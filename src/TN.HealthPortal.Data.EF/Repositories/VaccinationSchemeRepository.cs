using Microsoft.EntityFrameworkCore;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Data.EF.Repositories
{
    internal class VaccinationSchemeRepository : Repository<VaccinationScheme>, IVaccinationSchemeRepository
    {
        public VaccinationSchemeRepository(AppDbContext context) : base(context)
        {
            var dbSet = context.Set<VaccinationScheme>();
            dbSet.Include(scheme => scheme.Pathogen).ToList();
            dbSet.Include(scheme => scheme.Product).ThenInclude(product => product.Manufacturer).ToList();
        }
    }
}
