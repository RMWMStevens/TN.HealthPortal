using TN.HealthPortal.Lib.Entities;
using TN.HealthPortal.Lib.Repositories;

namespace TN.HealthPortal.Lib.Services
{
    public class FarmService : IFarmService
    {
        readonly IFarmRepository farmRepository;

        public FarmService(IFarmRepository farmRepository)
        {
            this.farmRepository = farmRepository;
        }

        public async Task AddAsync(Farm farm)
        {
            await farmRepository.AddAsync(farm);
        }

        public async Task DeleteByBlnNumberAsync(string blnNumber)
        {
            var farm = await GetByBlnNumberAsync(blnNumber);
            await farmRepository.RemoveAsync(farm);
        }

        public Task<Farm> GetByBlnNumberAsync(string blnNumber)
            => farmRepository.FindOneAsync(_ => _.BlnNumber == blnNumber);
    }
}
