using Microsoft.EntityFrameworkCore;
using TN.HealthPortal.Data.EF.Repositories.Generic;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Data.EF.Repositories
{
    internal class DewormingSchemeRepository : Repository<DewormingScheme>, IDewormingSchemeRepository
    {
        public DewormingSchemeRepository(AppDbContext context) : base(context)
        {
            var dbSet = context.Set<DewormingScheme>();
            dbSet.Include(scheme => scheme.Product).ThenInclude(product => product.Manufacturer).ToList();
        }
    }
}
