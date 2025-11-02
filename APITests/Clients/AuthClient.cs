using RestSharp;
using ApiTests.Constants;
using ApiTests.Models;

namespace ApiTests.Clients;

/// <summary>Handles authentication (register/login) API calls.</summary>
public class AuthClient : IAuthClient, IDisposable
{
    private readonly RestClient client;

    public AuthClient()
    {
        client = new RestClient(Endpoints.BaseUrl);
        client.AddDefaultHeader(Endpoints.ApiKeyHeader, Endpoints.ApiKeyValue);
    }

    public void Dispose() => client.Dispose();

    // https://reqres.in/api/register
    public async Task<RestResponse> Register(AuthRequest authRequest) => await client.ExecuteAsync(new RestRequest("register", Method.Post).AddJsonBody(authRequest));

    // https://reqres.in/api/login
    public async Task<RestResponse> Login(AuthRequest authRequest) => await client.ExecuteAsync(new RestRequest("login", Method.Post).AddJsonBody(authRequest));
}