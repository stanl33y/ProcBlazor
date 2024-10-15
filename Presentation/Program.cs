using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProcBlazor.Presentation;
using ProcBlazor.Presentation.Handlers;
using ProcBlazor.Presentation.Providers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<JwtAuthorizationMessageHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, StAuthStateProvider>();

string apiURL = builder.Configuration["APIURL"] ?? "http://localhost:5000";

builder.Services.AddHttpClient("APIClient", client =>
{
    client.BaseAddress = new Uri(apiURL);
})
.AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("APIClient"));

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<StAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<StAuthStateProvider>());
builder.Services.AddCascadingAuthenticationState();

await builder.Build().RunAsync();
