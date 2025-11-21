using RestSharp;
using ApiTests.Constants;

namespace ApiTests.Clients;

/// <summary>Implements API calls for resource endpoints (list, single, not found).</summary>
public class ResourceClient : BaseClient, IResourceClient
{
    // https://reqres.in/api/unknown
    public async Task<RestResponse> GetResources() => await client.ExecuteAsync(new RestRequest(Endpoints.Resources));

    // https://reqres.in/api/unknown/2
    public async Task<RestResponse> GetResource(int resourceId) => await client.ExecuteAsync(new RestRequest(Endpoints.SingleResource).AddUrlSegment("id", resourceId));

    // https://reqres.in/api/unknown/23
    public async Task<RestResponse> GetInvalidResource() => await client.ExecuteAsync(new RestRequest(Endpoints.SingleResource).AddUrlSegment("id", Endpoints.InvalidResource));
}