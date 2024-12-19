using Shared.Models;

namespace BlazorServer.Client.HttpClientHelper;

public interface IRestuarantService
{
    Task<Restuarant[]> Get();
    Task<Restuarant> Get(string id);
    Task<bool> Create(Restuarant restuarant);
    Task<bool> Update(Restuarant restuarant);
}
