using BlazorServer;
using BlazorServer.Client.HttpClientHelper;
using BlazorServer.Components;
using BlazorServer.HttpClientHelper;
using BlazorServer.RouteExtensions;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Components.Tooltip;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

const string httpClientName = "RestuarantApi";
string[] scopes = builder.Configuration.GetSection("RestuarantApi:Scopes").Get<string[]>() ?? ["user.read"];

builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration)
    .EnableTokenAcquisitionToCallDownstreamApi(scopes)
    .AddInMemoryTokenCaches();

builder.Services.AddDownstreamApi(httpClientName, builder.Configuration.GetSection(httpClientName));

builder.Services.AddAuthorization();

builder.Services.AddFluentUIComponents();

builder.Services.AddScoped<IRestuarantService, ServerRestuarantService>();
builder.Services.AddScoped<ITooltipService, TooltipService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRestuarantRoutes();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorServer.Client._Imports).Assembly);

app.MapGroup("/authentication").MapLoginAndLogout();

app.Run();
