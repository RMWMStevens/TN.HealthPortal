using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Extensions;
using TN.HealthPortal.Logic.Generators;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Logic.Services
{
    public class FarmService : IFarmService
    {
        private readonly IFarmRepository farmRepository;
        private readonly IRegionRepository regionRepository;
        private readonly ICountryRepository countryRepository;
        private readonly IFarmExportGenerator farmExportGenerator;
        private readonly IVaccinationSchemeRepository vaccinationSchemeRepository;
        private readonly IDewormingSchemeRepository dewormingSchemeRepository;

        public FarmService(
            IFarmRepository farmRepository,
            IRegionRepository regionRepository,
            ICountryRepository countryRepository,
            IVaccinationSchemeRepository vaccinationSchemeRepository,
            IDewormingSchemeRepository dewormingSchemeRepository,
            IFarmExportGenerator farmExportGenerator)
        {
            this.farmRepository = farmRepository;
            this.regionRepository = regionRepository;
            this.countryRepository = countryRepository;
            this.farmExportGenerator = farmExportGenerator;
            this.vaccinationSchemeRepository = vaccinationSchemeRepository;
            this.dewormingSchemeRepository = dewormingSchemeRepository;
        }

        public async Task AddAsync(Farm farm)
        {
            var dbCountry = await countryRepository.GetSingleAsync(country
                => country.Name == farm.Country.Name);
            var dbRegion = await regionRepository.GetSingleAsync(region
                => region.Name == farm.Country.Region.Name);

            if (dbCountry == null || dbRegion == null)
                throw new ArgumentException($"This farm's country '{farm.Country.Name}' or region '{farm.Country.Region.Name}' is not valid", nameof(farm));

            farm.Country = dbCountry;
            await farmRepository.AddAsync(farm);
        }

        public async Task<IEnumerable<Farm>> GetAllAsync(Veterinarian veterinarian)
        {
            if (veterinarian == null)
                throw new ArgumentException("Veterinarian is not valid");

            var farms = await farmRepository.GetAsync(farm
                => farm.Veterinarians.Contains(veterinarian)
                || veterinarian.Countries.Contains(farm.Country)
                || veterinarian.Regions.Contains(farm.Country.Region));

            return farms;
        }

        public async Task<IEnumerable<Farm>> GetAllOutdatedAsync(Veterinarian veterinarian)
        {
            var farms = await GetAllAsync(veterinarian);
            var outdatedFarms = farms.Where(farm
                => farm.ManuallyUpdatedAt < DateTime.UtcNow.AddYears(-1));

            return outdatedFarms;
        }

        public async Task<Farm?> GetByBlnNumberAsync(string blnNumber)
            => await farmRepository.GetSingleAsync(farm
                => farm.BlnNumber == blnNumber);

        public async Task DeleteByBlnNumberAsync(string blnNumber)
        {
            var farm = await GetByBlnNumberAsync(blnNumber);
            if (farm == null)
                throw new ArgumentException($"No farm found with BLN number {blnNumber}", nameof(blnNumber));

            await farmRepository.RemoveAsync(farm);
        }

        public async Task<byte[]> GeneratePdfHealthReportAsync(string blnNumber)
        {
            var farm = await GetByBlnNumberAsync(blnNumber);
            if (farm == null)
                throw new ArgumentException($"No farm found with BLN number {blnNumber}", nameof(blnNumber));

            var dewormingSchemes = await dewormingSchemeRepository.GetAsync(scheme
                => scheme.FarmBlnNumber == blnNumber);
            var vaccinationSchemes = await vaccinationSchemeRepository.GetAsync(scheme
                => scheme.FarmBlnNumber == blnNumber);
            farm.DewormingSchemes = dewormingSchemes.Order().ToList();
            farm.VaccinationSchemes = vaccinationSchemes.Order().ToList();

            return farmExportGenerator.Generate(farm);
        }
    }
}
