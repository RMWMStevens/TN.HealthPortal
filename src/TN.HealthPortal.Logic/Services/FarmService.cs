using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Logic.Services
{
    public class FarmService : IFarmService
    {
        readonly IFarmRepository farmRepository;
        readonly IRegionRepository regionRepository;
        readonly ICountryRepository countryRepository;

        public FarmService(
            IFarmRepository farmRepository,
            IRegionRepository regionRepository,
            ICountryRepository countryRepository)
        {
            this.farmRepository = farmRepository;
            this.regionRepository = regionRepository;
            this.countryRepository = countryRepository;
        }

        public async Task AddAsync(Farm farm)
        {
            var dbCountry = (await countryRepository.GetAsync(country => country.Name == farm.Country.Name)).FirstOrDefault();
            var dbRegion = (await regionRepository.GetAsync(region => region.Name == farm.Country.Region.Name)).FirstOrDefault();

            if (dbCountry == null || dbRegion == null)
                throw new ArgumentException("This farm's country or region is not valid", nameof(farm));

            farm.Country = dbCountry;
            await farmRepository.AddAsync(farm);
        }

        public async Task<IEnumerable<Farm>> GetAllAsync(Veterinarian veterinarian)
            => await farmRepository.GetAsync(
                farm => farm.Veterinarians.Contains(veterinarian)
                || veterinarian.Countries.Contains(farm.Country)
                || veterinarian.Regions.Contains(farm.Country.Region));

        public async Task<IEnumerable<Farm>> GetAllOutdatedAsync(Veterinarian veterinarian)
            => await farmRepository.GetAsync(
                farm => (farm.Veterinarians.Contains(veterinarian)
                || veterinarian.Countries.Contains(farm.Country)
                || veterinarian.Regions.Contains(farm.Country.Region))
                && farm.ManuallyUpdatedAt < DateTime.UtcNow.AddYears(-1));

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
