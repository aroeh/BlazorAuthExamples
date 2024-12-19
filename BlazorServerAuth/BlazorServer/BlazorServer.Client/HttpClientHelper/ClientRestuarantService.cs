using Shared.Models;
using System.Net;
using System.Net.Http.Json;

namespace BlazorServer.Client.HttpClientHelper;

internal sealed class ClientRestuarantService(HttpClient http) : IRestuarantService
{
    public async Task<Restuarant[]> Get() => await http.GetFromJsonAsync<Restuarant[]>("/restuarant") ?? [];

    public async Task<Restuarant> Get(string id) => await http.GetFromJsonAsync<Restuarant>($"/restuarant/{id}") ?? new();

    public async Task<bool> Create(Restuarant restuarant)
    {
        HttpResponseMessage result = await http.PostAsJsonAsync<Restuarant>("/restuarant", restuarant);
        return result.IsSuccessStatusCode && (result.StatusCode.Equals(HttpStatusCode.Created) || result.StatusCode.Equals(HttpStatusCode.OK));
    }

    public async Task<bool> Update(Restuarant restuarant)
    {
        HttpResponseMessage result = await http.PostAsJsonAsync<Restuarant>("/restuarant", restuarant);
        return result.IsSuccessStatusCode && result.StatusCode.Equals(HttpStatusCode.OK);
    }
}
