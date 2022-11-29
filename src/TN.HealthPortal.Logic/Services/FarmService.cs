using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Logic.Services
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

        public Task<IEnumerable<Farm>> GetAll(Veterinarian veterinarian)
            => farmRepository.GetAsync(
                farm => farm.Veterinarians.Contains(veterinarian)
                || veterinarian.Regions.Contains(farm.Country.Region));

        public async Task<Farm?> GetByBlnNumberAsync(string blnNumber)
            => (await farmRepository.GetAsync(farm => farm.BlnNumber == blnNumber)).FirstOrDefault();

        public async Task DeleteByBlnNumberAsync(string blnNumber)
        {
            var farm = await GetByBlnNumberAsync(blnNumber);
            if (farm != null)
                await farmRepository.RemoveAsync(farm);
        }
    }
}
