using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProcBlazor.Presentation;
using ProcBlazor.Presentation.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<JwtAuthorizationMessageHandler>();

string apiURL = builder.Configuration["APIURL"] ?? "http://localhost:5000";

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiURL)
});

builder.Services.AddHttpClient("APIClient", client =>
{
    client.BaseAddress = new Uri(apiURL);
})
.AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

await builder.Build().RunAsync();
