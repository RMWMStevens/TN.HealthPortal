using System.Security.Claims;
using System.Security.Principal;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Services;

namespace TN.HealthPortal.API.Helpers
{
    public class IdentityHelper : IIdentityHelper
    {
        private readonly IVeterinarianService veterinarianService;

        public IdentityHelper(IVeterinarianService veterinarianService)
        {
            this.veterinarianService = veterinarianService;
        }

        public async Task<Veterinarian?> GetLoggedInVeterinarianAsync(IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity == null)
                return null;

            var employeeCode = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            if (employeeCode == null)
                return null;

            return await veterinarianService.GetByEmployeeCodeAsync(employeeCode);
        }
    }
}
