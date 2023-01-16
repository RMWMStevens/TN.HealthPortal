using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Logic.Services
{
    public class VaccinationSchemeService : IVaccinationSchemeService
    {
        private readonly IVaccinationSchemeRepository vaccinationSchemeRepository;
        private readonly IPathogenRepository pathogenRepository;
        private readonly IProductRepository productRepository;
        private readonly IManufacturerRepository manufacturerRepository;

        public VaccinationSchemeService(
            IVaccinationSchemeRepository vaccinationSchemeRepository,
            IPathogenRepository pathogenRepository,
            IProductRepository productRepository,
            IManufacturerRepository manufacturerRepository)
        {
            this.vaccinationSchemeRepository = vaccinationSchemeRepository;
            this.pathogenRepository = pathogenRepository;
            this.productRepository = productRepository;
            this.manufacturerRepository = manufacturerRepository;
        }

        public async Task AddAsync(VaccinationScheme vaccinationScheme)
        {
            var dbPathogen = await pathogenRepository.GetSingleAsync(pathogen
                => pathogen.Name == vaccinationScheme.Pathogen.Name);
            if (dbPathogen == null)
                throw new ArgumentException($"The pathogen '{vaccinationScheme.Pathogen.Name}' is not valid", nameof(vaccinationScheme));

            vaccinationScheme.Pathogen = dbPathogen;

            var dbProduct = await productRepository.GetSingleAsync(product
                => product.Name == vaccinationScheme.Product.Name);
            if (dbProduct == null)
            {
                var dbManufacturer = await manufacturerRepository.GetSingleAsync(manufacturer
                    => manufacturer.Name == vaccinationScheme.Product.Manufacturer.Name);
                if (dbManufacturer == null)
                    throw new ArgumentException($"The manufacturer '{vaccinationScheme.Product.Manufacturer.Name}' is not valid", nameof(vaccinationScheme));

                vaccinationScheme.Product.Manufacturer = dbManufacturer;
            }
            else
            {
                vaccinationScheme.Product = dbProduct;
            }

            await vaccinationSchemeRepository.AddAsync(vaccinationScheme);
        }

        public async Task<IEnumerable<VaccinationScheme>> GetByBlnNumberAsync(string blnNumber)
            => await vaccinationSchemeRepository.GetAsync(scheme
                => scheme.FarmBlnNumber == blnNumber);

        public async Task<IEnumerable<Manufacturer>?> GetManufacturerDropdownOptionsAsync()
            => await manufacturerRepository.GetAsync(manufacturer
                => !string.IsNullOrEmpty(manufacturer.Name));

        public async Task<IEnumerable<Product>?> GetProductDropdownOptionsAsync()
            => await productRepository.GetAsync(product
                => !string.IsNullOrEmpty(product.Name) && product.IsApproved);

        public async Task<IEnumerable<Pathogen>?> GetPathogenDropdownOptionsAsync()
            => await pathogenRepository.GetAsync(pathogen
                => !string.IsNullOrEmpty(pathogen.Name));
    }
}
