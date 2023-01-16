using TN.HealthPortal.Logic.Entities;

namespace TN.HealthPortal.Logic.Services
{
    public interface IDewormingSchemeService
    {
        Task AddAsync(DewormingScheme dewormingScheme);

        Task<IEnumerable<DewormingScheme>> GetByBlnNumberAsync(string blnNumber);

        Task<IEnumerable<Manufacturer>?> GetManufacturerDropdownOptionsAsync();

        Task<IEnumerable<Product>?> GetProductDropdownOptionsAsync();
    }
}
