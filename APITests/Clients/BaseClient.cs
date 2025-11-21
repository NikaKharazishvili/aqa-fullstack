using RestSharp;
using ApiTests.Constants;

namespace ApiTests.Clients;

/// <summary>Base class for all API clients – handles RestClient creation, default headers and disposal.</summary>
public abstract class BaseClient : IDisposable
{
    protected readonly RestClient client;

    protected BaseClient()
    {
        client = new RestClient(Endpoints.BaseUrl);
        client.AddDefaultHeader(Endpoints.ApiKeyHeader, Endpoints.ApiKeyValue);
    }

    public void Dispose() => client.Dispose();
}