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

        public void Add(Farm farm)
        {
            farmRepository.Add(farm);
        }

        public Farm GetByBlnNumber(string blnNumber) => farmRepository
            .Find(_ => _.BlnNumber == blnNumber).First();
    }
}
