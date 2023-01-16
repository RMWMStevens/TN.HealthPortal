using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Logic.Services
{
    public class DewormingSchemeService : IDewormingSchemeService
    {
        private readonly IDewormingSchemeRepository dewormingSchemeRepository;
        private readonly IProductRepository productRepository;
        private readonly IManufacturerRepository manufacturerRepository;

        public DewormingSchemeService(
            IDewormingSchemeRepository dewormingSchemeRepository,
            IProductRepository productRepository,
            IManufacturerRepository manufacturerRepository)
        {
            this.dewormingSchemeRepository = dewormingSchemeRepository;
            this.productRepository = productRepository;
            this.manufacturerRepository = manufacturerRepository;
        }

        public async Task AddAsync(DewormingScheme dewormingScheme)
        {
            var dbProduct = await productRepository.GetSingleAsync(product
                => product.Name == dewormingScheme.Product.Name);
            if (dbProduct == null)
            {
                var dbManufacturer = await manufacturerRepository.GetSingleAsync(manufacturer
                    => manufacturer.Name == dewormingScheme.Product.Manufacturer.Name);
                if (dbManufacturer == null)
                    throw new ArgumentException($"The manufacturer '{dewormingScheme.Product.Manufacturer.Name}' is not valid", nameof(dewormingScheme));

                dewormingScheme.Product.Manufacturer = dbManufacturer;
            }
            else
            {
                dewormingScheme.Product = dbProduct;
            }

            await dewormingSchemeRepository.AddAsync(dewormingScheme);
        }

        public async Task<IEnumerable<DewormingScheme>> GetByBlnNumberAsync(string blnNumber)
            => await dewormingSchemeRepository.GetAsync(scheme
                => scheme.FarmBlnNumber == blnNumber);

        public async Task<IEnumerable<Manufacturer>?> GetManufacturerDropdownOptionsAsync()
            => await manufacturerRepository.GetAsync(manufacturer
                => !string.IsNullOrEmpty(manufacturer.Name));

        public async Task<IEnumerable<Product>?> GetProductDropdownOptionsAsync()
            => await productRepository.GetAsync(product
                => !string.IsNullOrEmpty(product.Name) && product.IsApproved);
    }
}
