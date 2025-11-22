using RestSharp;
using ApiTests.Constants;

namespace ApiTests.Clients;

/// <summary>Base class for all API clients – handles RestClient creation, default headers and disposal.</summary>
public abstract class BaseClient : IDisposable
{
    protected readonly RestClient _client;

    protected BaseClient()
    {
        _client = new RestClient(Endpoints.BaseUrl);
        _client.AddDefaultHeader(Endpoints.ApiKeyHeader, Endpoints.ApiKeyValue);
    }

    public void Dispose() => _client.Dispose();
}