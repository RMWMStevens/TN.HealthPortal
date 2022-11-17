using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Data.EF.Repositories
{
    internal class FarmRepository : Repository<Farm>, IFarmRepository
    {
        public FarmRepository(AppDbContext context) : base(context) { }
    }
}
