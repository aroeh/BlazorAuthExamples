using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components.Components.Tooltip;
using Microsoft.FluentUI.AspNetCore.Components;
using BlazorServer.Client.HttpClientHelper;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

builder.Services.AddFluentUIComponents();

builder.Services.AddScoped<ITooltipService, TooltipService>();

/*
 * register a client side Http client with a base address that is the same as the host.
 * When invoked the client side HTTP client will call to the an endpoint on the Blazor Server app 
 * and it will be the server app that will initiate the HTTP request to the external API
 */
builder.Services.AddHttpClient<IRestuarantService, ClientRestuarantService>(options =>
{
    options.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

await builder.Build().RunAsync();
