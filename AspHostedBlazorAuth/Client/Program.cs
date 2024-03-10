using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AspHostedBlazorAuth.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

const string httpClientName = "AspHostedBlazorAuth.ServerAPI";
// By default the BaseAddressAuthorizationMessageHandler will add token info to the http request because
// the client and api are all on the same domain/resources
builder.Services.AddHttpClient(httpClientName, client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName));

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    string scope = builder.Configuration["Scope"] ?? "user.read";
    options.ProviderOptions.DefaultAccessTokenScopes.Add(scope);
});

await builder.Build().RunAsync();
