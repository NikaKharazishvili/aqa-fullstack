using RestSharp;
using ApiTests.Models;

namespace ApiTests.Clients;

public interface IUsersClient
{
    Task<RestResponse> GetUsers(int page);
    Task<RestResponse> GetUser(int userId);
    Task<RestResponse> CreateUser(UserRequest user);
    Task<RestResponse> UpdateUser(int userId, UserRequest user);
    Task<RestResponse> DeleteUser(int userId);
}