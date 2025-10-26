using RestSharp;
using ApiTests.Constants;
using ApiTests.Models;

namespace ApiTests.Clients;

public class UsersClient : IUsersClient
{
    private readonly RestClient client;

    public UsersClient()
    {
        client = new RestClient(Endpoints.BaseUrl);
        client.AddDefaultHeader(Endpoints.ApiKeyHeader, Endpoints.ApiKeyValue);
    }

    // https://reqres.in/api/users?page=2
    public async Task<RestResponse> GetUsers(int pageId) => await client.ExecuteAsync(new RestRequest(Endpoints.Users).AddQueryParameter("page", pageId));

    // https://reqres.in/api/users/2
    public async Task<RestResponse> GetUser(int userId) => await client.ExecuteAsync(new RestRequest(Endpoints.SingleUser).AddUrlSegment("id", userId));

    // https://reqres.in/api/users   { "name": "morpheus", "job": "leader" }
    public async Task<RestResponse> CreateUser(UserRequest user) => await client.ExecuteAsync(new RestRequest(Endpoints.Users, Method.Post).AddJsonBody(user));

    // https://reqres.in/api/users/2   { "name": "morpheus", "job": "zion resident" }
    public async Task<RestResponse> UpdateUser(int userId, UserRequest user) => await client.ExecuteAsync(new RestRequest(Endpoints.SingleUser, Method.Put).AddUrlSegment("id", userId).AddJsonBody(user));

    // https://reqres.in/api/users/2   { "name": "morpheus", "job": "zion resident" }
    public async Task<RestResponse> UpdateUser2(int userId, UserRequest user) => await client.ExecuteAsync(new RestRequest(Endpoints.SingleUser, Method.Patch).AddUrlSegment("id", userId).AddJsonBody(user));

    // https://reqres.in/api/users/2
    public async Task<RestResponse> DeleteUser(int userId) => await client.ExecuteAsync(new RestRequest(Endpoints.SingleUser, Method.Delete).AddUrlSegment("id", userId));
}