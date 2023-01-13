using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Logic.Services
{
    public class VaccinationSchemeService : IVaccinationSchemeService
    {
        readonly IVaccinationSchemeRepository vaccinationSchemeRepository;
        readonly IPathogenRepository pathogenRepository;
        readonly IProductRepository productRepository;

        public VaccinationSchemeService(IVaccinationSchemeRepository vaccinationSchemeRepository, IPathogenRepository pathogenRepository, IProductRepository productRepository)
        {
            this.vaccinationSchemeRepository = vaccinationSchemeRepository;
            this.pathogenRepository = pathogenRepository;
            this.productRepository = productRepository;
        }

        public async Task AddAsync(VaccinationScheme vaccinationScheme)
        {
            var dbPathogen = (await pathogenRepository.GetAsync(pathogen => pathogen.Name == vaccinationScheme.Pathogen.Name)).FirstOrDefault();
            var dbProduct = (await productRepository.GetAsync(product => product.Name == vaccinationScheme.Product.Name)).FirstOrDefault();

            if (dbPathogen == null)
                throw new ArgumentException("This pathogen is not valid", nameof(vaccinationScheme));

            if (dbProduct == null)
                throw new ArgumentException("This product is not valid", nameof(vaccinationScheme));

            vaccinationScheme.Pathogen = dbPathogen;
            vaccinationScheme.Product = dbProduct;

            await vaccinationSchemeRepository.AddAsync(vaccinationScheme);
        }

        public async Task<IEnumerable<VaccinationScheme>> GetByBlnNumberAsync(string blnNumber)
                => await vaccinationSchemeRepository.GetAsync(_ => _.FarmBlnNumber == blnNumber);
    }
}
