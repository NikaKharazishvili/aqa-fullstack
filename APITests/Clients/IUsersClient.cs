using RestSharp;
using ApiTests.Models;

namespace ApiTests.Clients;

public interface IUsersClient
{
    Task<RestResponse> GetUsers(int page);
    Task<RestResponse> GetUser(string userId);
    Task<RestResponse> CreateUser(UserRequest user);
    Task<RestResponse> UpdateUser(string userId, UserRequest user);
    Task<RestResponse> DeleteUser(string userId);
}