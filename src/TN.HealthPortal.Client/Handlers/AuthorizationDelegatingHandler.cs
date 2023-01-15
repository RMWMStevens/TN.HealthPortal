using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TN.HealthPortal.Client.Extensions;
using TN.HealthPortal.Logic.DTOs.Authentication;

namespace TN.HealthPortal.Client.Handlers
{
    public class AuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly ILocalStorageService localStorageService;

        private string token = string.Empty;
        private string tokenEmployeeCode = string.Empty;
        private DateTime tokenExpiryDate = DateTime.MinValue;

        public AuthorizationDelegatingHandler(
            IHttpClientFactory httpClientFactory,
            ILocalStorageService localStorageService,
            IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await GetAccessTokenAsync(cancellationToken);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken)
        {
            string employeeCode = await localStorageService.GetItemAsStringAsync("employeeCode", cancellationToken);

            if (!string.IsNullOrEmpty(token)
                && tokenExpiryDate > DateTime.UtcNow
                && employeeCode == tokenEmployeeCode)
                return token;

            var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.PostAsJsonAsync(
                configuration["Api:TokenUrl"],
                new LoginDto { EmployeeCode = employeeCode },
                cancellationToken);
            response.EnsureSuccessStatusCode();
            var result = (await response.Content.ReadAsStringAsync()).FromJson<TokenDto>();
            token = result.Token;
            tokenExpiryDate = result.ExpiresAt;
            return token;
        }
    }
}
