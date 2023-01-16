using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Logic.Services
{
    public class VeterinarianService : IVeterinarianService
    {
        private readonly IVeterinarianRepository veterinarianRepository;

        public VeterinarianService(IVeterinarianRepository veterinarianRepository)
        {
            this.veterinarianRepository = veterinarianRepository;
        }

        public async Task<Veterinarian?> GetByEmployeeCodeAsync(string employeeCode)
            => await veterinarianRepository.GetSingleAsync(vet
                => vet.EmployeeCode == employeeCode);
    }
}
