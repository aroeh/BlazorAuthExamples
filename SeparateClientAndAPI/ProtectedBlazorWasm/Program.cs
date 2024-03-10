using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProtectedBlazorWasm;
using ProtectedBlazorWasm.CustomAuthHandlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CustomAuthorizationMessageHandler>();

// Retrieve the base api uri from wwwroot appsettings.json
string baseApiUri = builder.Configuration["BaseApiUri"] ?? "https://localhost";

const string httpClientName = "ProtectedWebApi";
// a custom handler is needed to add bearer tokens to apis outside of the client's base domain/resources
builder.Services.AddHttpClient(httpClientName, client => client.BaseAddress = new Uri(baseApiUri))
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName));

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    string scope = builder.Configuration["Scope"] ?? "user.read";
    options.ProviderOptions.DefaultAccessTokenScopes.Add(scope);
});

await builder.Build().RunAsync();
