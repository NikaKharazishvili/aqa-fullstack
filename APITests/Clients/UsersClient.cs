using RestSharp;
using ApiTests.Constants;
using ApiTests.Models;

namespace ApiTests.Clients;

public class UsersClient : IUsersClient
{
    private readonly RestClient Client;

    public UsersClient()
    {
        Client = new RestClient(Endpoints.BaseUrl);
        Client.AddDefaultHeader(Endpoints.ApiKeyHeader, Endpoints.ApiKeyValue);
    }

    public async Task<RestResponse> GetUsers(int page)
    {
        var request = new RestRequest(Endpoints.Users)
            .AddParameter("page", page, ParameterType.QueryString);

        return await Client.ExecuteAsync(request);
    }

    // Implement others later...
    public Task<RestResponse> GetUser(string userId) => throw new NotImplementedException();
    public Task<RestResponse> CreateUser(UserRequest user) => throw new NotImplementedException();
    public Task<RestResponse> UpdateUser(string userId, UserRequest user) => throw new NotImplementedException();
    public Task<RestResponse> DeleteUser(string userId) => throw new NotImplementedException();
}