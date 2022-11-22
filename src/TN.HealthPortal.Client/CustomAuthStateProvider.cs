using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace TN.HealthPortal.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;

        public CustomAuthStateProvider(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var state = new AuthenticationState(new ClaimsPrincipal());

            string employeeCode = await localStorageService.GetItemAsStringAsync("employeeCode");
            if (!string.IsNullOrEmpty(employeeCode))
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, employeeCode)
                }, "test authentication type");

                state = new AuthenticationState(new ClaimsPrincipal(identity));
            }

            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }
    }
}
