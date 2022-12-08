using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Data.EF.Repositories
{
    internal class PathogenRepository : Repository<Pathogen>, IPathogenRepository
    {
        public PathogenRepository(AppDbContext context) : base(context) { }
    }
}
