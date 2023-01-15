using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TN.HealthPortal.Client;
using TN.HealthPortal.Client.Handlers;
using TN.HealthPortal.Client.Providers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AuthorizationDelegatingHandler>();
builder.Services.AddHttpClient("Api", _ => _.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]))
    .AddHttpMessageHandler<AuthorizationDelegatingHandler>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();