using RestSharp;
using ApiTests.Models;

namespace ApiTests.Clients;

public interface IAuthClient
{
    Task<RestResponse> Register(AuthRequest authRequest);
    Task<RestResponse> Login(AuthRequest authRequest);
}