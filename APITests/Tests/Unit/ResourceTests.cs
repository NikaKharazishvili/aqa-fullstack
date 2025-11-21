using Moq;
using RestSharp;
using Newtonsoft.Json.Linq;
using ApiTests.Clients;
using static System.Net.HttpStatusCode;
using static Shared.Utils;

namespace ApiTests.Tests.Unit;

/// <summary>Unit tests mocking resource client with predefined JSON.</summary>
[TestFixture]
[Category(UNIT)]
[Category(API)]
[Parallelizable(ParallelScope.Fixtures)]
public class ResourceTests
{
    private Mock<IResourceClient> _mockClient;

    [SetUp] public void Setup() => _mockClient = new Mock<IResourceClient>();

    [Test]
    [Description("Unit Test → Simulated GET /unknown returns correct data")]
    public async Task GetListResources_ReturnsExpectedJson()
    {
        var fakeResponse = new RestResponse { StatusCode = OK, Content = LoadEmbeddedText("Tests/Unit/TestData/ListResources.json") };
        _mockClient.Setup(c => c.GetResources()).ReturnsAsync(fakeResponse);

        var response = await _mockClient.Object.GetResources();
        var parsed = JObject.Parse(response.Content!);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(OK));
            Assert.That(parsed["per_page"]?.Value<int>(), Is.EqualTo(6));
            Assert.That(parsed["data"]?[0]?["name"]?.Value<string>(), Is.EqualTo("cerulean"));
        });
    }

    [Test]
    [Description("Unit Test → Simulated GET /unknown/2 returns correct data")]
    public async Task GetResource_Id2_ReturnsExpectedJson()
    {
        var fakeResponse = new RestResponse { StatusCode = OK, Content = LoadEmbeddedText("Tests/Unit/TestData/GetResource_Id2.json") };
        _mockClient.Setup(c => c.GetResource(2)).ReturnsAsync(fakeResponse);

        var response = await _mockClient.Object.GetResource(2);
        var parsed = JObject.Parse(response.Content!);
        var data = parsed["data"] as JObject;

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(OK));
            Assert.That(data?["name"]?.Value<string>(), Is.EqualTo("fuchsia rose"));
            Assert.That(data?["year"]?.Value<int>(), Is.EqualTo(2001));
        });
    }

    [Test]
    [Description("Unit Test → Simulated GET /unknown/23 returns 404")]
    public async Task GetResource_NotFound_Returns404()
    {
        var fakeResponse = new RestResponse { StatusCode = NotFound, Content = LoadEmbeddedText("Tests/Unit/TestData/GetResource_NotFound.json") };
        _mockClient.Setup(c => c.GetInvalidResource()).ReturnsAsync(fakeResponse);

        var response = await _mockClient.Object.GetInvalidResource();

        Assert.That(response.StatusCode, Is.EqualTo(NotFound));
    }
}