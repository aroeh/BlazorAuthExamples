using BlazorServer.Client.HttpClientHelper;
using Microsoft.Identity.Web;
using Shared.Models;

namespace BlazorServer.HttpClientHelper;

public class ServerRestuarantService : HttpClientBase, IRestuarantService
{
    private readonly IConfiguration _config;
    private readonly ITokenAcquisition _tokenService;
    private readonly HttpClient _httpClient;

    public ServerRestuarantService(IConfiguration config, ILogger<ServerRestuarantService> log, ITokenAcquisition tokenAcquisition, HttpClient http)
        : base(log, http)
    {
        _config = config;
        _tokenService = tokenAcquisition;
        _httpClient = http;
        //_httpClient.BaseAddress = new Uri(_config["RestuarantApi:BaseUrl"]!);
        _httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("RESTUARANT_API") ?? "http://localhost");
        _httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
    }

    public async Task<Restuarant[]> Get()
    {
        await AcquireToken();
        return await GetAsync<Restuarant[]>("api/restuarant");
    }
    public async Task<Restuarant> Get(string id)
    {
        await AcquireToken();
        return await GetAsync<Restuarant>($"api/restuarant/{id}");
    }
    public async Task<bool> Create(Restuarant restuarant)
    {
        await AcquireToken();
        return await PostAsync<Restuarant, bool>("api/restuarant", restuarant);
    }
    public async Task<bool> Update(Restuarant restuarant)
    {
        await AcquireToken();
        return await PutAsync<Restuarant, bool>("api/restuarant", restuarant);
    }

    private async Task AcquireToken()
    {
        try
        {
            string[] scopes = _config.GetSection("RestuarantApi:Scopes").Get<string[]>() ?? ["user.read"];
            string token = await _tokenService.GetAccessTokenForUserAsync( scopes );

            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", token);
        }
        catch (Exception)
        {
            throw new Exception("Error getting access token.");
        }
    }
}
