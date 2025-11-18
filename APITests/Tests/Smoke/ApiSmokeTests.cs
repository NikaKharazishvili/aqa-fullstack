using ApiTests.Clients;
using static System.Net.HttpStatusCode;
using static Shared.Utils;

namespace ApiTests.Tests.Smoke;

/// <summary>Smoke tests for API endpoint availability.</summary>
[TestFixture]
[Category(SMOKE)]
[Category(API)]
[Parallelizable(ParallelScope.All)]
public class ApiSmokeTests
{
    [Test]
    [Description("GET /unknown → API is reachable...")]
    public async Task ResourceList_IsReachable()
    {
        using var client = new ResourceClient();
        var response = await client.GetResources();
        Assert.That(response.StatusCode, Is.EqualTo(OK));
    }

    [Test]
    [Description("GET /users?page=1 → Users endpoint...")]
    public async Task UsersList_IsReachable()
    {
        using var client = new UserClient();
        var response = await client.GetUsers(1);
        Assert.That(response.StatusCode, Is.EqualTo(OK));
    }
}