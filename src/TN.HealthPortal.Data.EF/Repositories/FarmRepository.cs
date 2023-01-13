using Microsoft.EntityFrameworkCore;
using TN.HealthPortal.Data.EF.Repositories.Generic;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Data.EF.Repositories
{
    internal class FarmRepository : Repository<Farm>, IFarmRepository
    {
        public FarmRepository(AppDbContext context) : base(context)
        {
            var dbSet = context.Farms;
            dbSet.Include(farm => farm.Country).ToList();
            dbSet.Include(farm => farm.Address).ToList();
            dbSet.Include(farm => farm.ProductionTypes).ToList();
            dbSet.Include(farm => farm.Sources).ToList();
            dbSet.Include(farm => farm.DiseaseStatuses).ToList();
        }
    }
}
