using TN.HealthPortal.Data.EF.Repositories.Generic;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Data.EF.Repositories
{
    internal class RegionRepository : Repository<Region>, IRegionRepository
    {
        public RegionRepository(AppDbContext context) : base(context) { }
    }
}
