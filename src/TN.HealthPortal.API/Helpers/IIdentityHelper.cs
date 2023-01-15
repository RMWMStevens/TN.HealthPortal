using System.Security.Principal;
using TN.HealthPortal.Logic.Entities;

namespace TN.HealthPortal.API.Helpers
{
    public interface IIdentityHelper
    {
        Task<Veterinarian?> GetLoggedInVeterinarianAsync(IIdentity identity);
    }
}
