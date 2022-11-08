using TN.HealthPortal.Lib.Entities;
using TN.HealthPortal.Lib.Repositories;

namespace TN.HealthPortal.Data.EF.Repositories
{
    internal class FarmRepository : Repository<Farm>, IFarmRepository
    {
        public FarmRepository(AppDbContext context) : base(context) { }
    }
}
