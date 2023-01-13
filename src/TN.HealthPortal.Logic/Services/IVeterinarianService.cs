using TN.HealthPortal.Logic.Entities;

namespace TN.HealthPortal.Logic.Services
{
    public interface IVeterinarianService
    {
        Task<Veterinarian?> GetByEmployeeCodeAsync(string employeeCode);
    }
}
