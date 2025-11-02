using RestSharp;
using ApiTests.Models;

namespace ApiTests.Clients;

/// <summary>Defines contract for authentication operations.</summary>
public interface IAuthClient
{
    Task<RestResponse> Register(AuthRequest authRequest);
    Task<RestResponse> Login(AuthRequest authRequest);
}