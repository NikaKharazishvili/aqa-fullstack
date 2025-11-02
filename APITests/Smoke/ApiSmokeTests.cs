using RestSharp;
using ApiTests.Constants;
using static System.Net.HttpStatusCode;
using static Shared.Utils;

namespace ApiTests.Smoke;

/// <summary>Smoke tests for API endpoint availability.</summary>
[TestFixture]
[Category(SMOKE)]
[Category(API)]
[Parallelizable(ParallelScope.All)]
public class ApiSmokeTests
{
    [Test]
    [Description("SMOKE: GET /unknown → API is reachable and returns 200")]
    public async Task ResourceList_IsReachable()
    {
        using var client = new RestClient(Endpoints.BaseUrl);
        var response = await client.ExecuteAsync(new RestRequest("unknown"));
        Assert.That(response.StatusCode, Is.EqualTo(OK));
    }

    [Test]
    [Description("SMOKE: GET /users?page=1 → Users endpoint is alive")]
    public async Task UsersList_IsReachable()
    {
        using var client = new RestClient(Endpoints.BaseUrl);
        var response = await client.ExecuteAsync(new RestRequest("users").AddParameter("page", 1));
        Assert.That(response.StatusCode, Is.EqualTo(OK));
    }
}