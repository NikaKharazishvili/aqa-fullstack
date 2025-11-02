using RestSharp;

namespace ApiTests.Clients;

/// <summary>Defines contract for resource CRUD operations.</summary>
public interface IResourceClient
{
    Task<RestResponse> GetResources();
    Task<RestResponse> GetResource(int resourceId);
    Task<RestResponse> GetInvalidResource();
}