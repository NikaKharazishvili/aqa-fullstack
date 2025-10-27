using RestSharp;

namespace ApiTests.Clients;

public interface IResourceClient
{
    Task<RestResponse> GetResources();
    Task<RestResponse> GetResource(int resourceId);
    Task<RestResponse> GetInvalidResource();
}