using RestSharp;
using ApiTests.Constants;
using ApiTests.Models;

namespace ApiTests.Clients;

/// <summary>Implements API calls for user management (CRUD + delay).</summary>
public class UserClient : BaseClient, IUserClient
{
    // https://reqres.in/api/users?page=2
    public async Task<RestResponse> GetUsers(int pageId) => await client.ExecuteAsync(new RestRequest(Endpoints.Users).AddQueryParameter("page", pageId));

    // https://reqres.in/api/users/2
    public async Task<RestResponse> GetUser(int userId) => await client.ExecuteAsync(new RestRequest(Endpoints.SingleUser).AddUrlSegment("id", userId));

    // https://reqres.in/api/users   { "name": "morpheus", "job": "leader" }
    public async Task<RestResponse> CreateUser(UserRequest user) => await client.ExecuteAsync(new RestRequest(Endpoints.CreateUser, Method.Post).AddJsonBody(user));

    // https://reqres.in/api/users/2   { "name": "morpheus", "job": "zion resident" }
    public async Task<RestResponse> UpdateUser(int userId, UserRequest user) => await client.ExecuteAsync(new RestRequest(Endpoints.UpdateUser, Method.Put).AddUrlSegment("id", userId).AddJsonBody(user));

    // https://reqres.in/api/users/2   { "name": "morpheus", "job": "zion resident" }
    public async Task<RestResponse> PatchUser(int userId, UserRequest user) => await client.ExecuteAsync(new RestRequest(Endpoints.UpdateUser, Method.Patch).AddUrlSegment("id", userId).AddJsonBody(user));

    // https://reqres.in/api/users/2
    public async Task<RestResponse> DeleteUser(int userId) => await client.ExecuteAsync(new RestRequest(Endpoints.DeleteUser, Method.Delete).AddUrlSegment("id", userId));

    // https://reqres.in/api/users/?delay=2
    public async Task<RestResponse> DelayedResponse(int seconds) => await client.ExecuteAsync(new RestRequest(Endpoints.DelayedResponse).AddQueryParameter("delay", seconds));
}