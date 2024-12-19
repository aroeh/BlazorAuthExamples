using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using ProtectedApi.DataAccess;
using ProtectedApi.DomainLogic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IRestuarantLogic, RestuarantLogic>();
builder.Services.AddTransient<IRestuarantData, RestuarantData>();
builder.Services.AddTransient<IMongoDbWrapper, MongoDbWrapper>();

const string corsPolicyName = "ApiCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
        policy =>
        {
            string[] allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? ["localhost"];
            string[] allowedHeaders = builder.Configuration.GetSection("AllowedHeaders").Get<string[]>() ?? ["Authorization", "Content-Type", "Accept"];
            string[] allowedMethods = builder.Configuration.GetSection("AllowedMethods").Get<string[]>() ?? ["GET"];
            policy.WithOrigins(allowedOrigins);
            policy.WithHeaders(allowedHeaders);
            policy.WithMethods(allowedMethods);
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
