using RestSharp;
using ApiTests.Models;

namespace ApiTests.Clients;

/// <summary>Handles authentication (register/login) API calls.</summary>
public class AuthClient : BaseClient, IAuthClient
{
    // https://reqres.in/api/register
    public async Task<RestResponse> Register(AuthRequest authRequest) => await _client.ExecuteAsync(new RestRequest("register", Method.Post).AddJsonBody(authRequest));

    // https://reqres.in/api/login
    public async Task<RestResponse> Login(AuthRequest authRequest) => await _client.ExecuteAsync(new RestRequest("login", Method.Post).AddJsonBody(authRequest));
}