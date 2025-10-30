using Moq;
using RestSharp;
using Newtonsoft.Json.Linq;
using ApiTests.Clients;
using ApiTests.Unit.Helpers;
using static System.Net.HttpStatusCode;
using static Shared.Utils;

namespace ApiTests.Unit;

[TestFixture]
[Category(UNIT)]
[Category(API)]
[Parallelizable(ParallelScope.All)]
public class ResourceTests
{
    private Mock<IResourceClient> _mockClient;

    [SetUp]
    public void Setup() => _mockClient = new Mock<IResourceClient>();

    [Test]
    [Order(1)]
    [Description("Unit Test → Simulated GET /unknown returns correct data")]
    public async Task GetListResources_ReturnsExpectedJson()
    {
        var fakeResponse = new RestResponse { StatusCode = OK, Content = JsonLoader.Load("ListResources.json") };
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
    [Order(2)]
    [Description("Unit Test → Simulated GET /unknown/2 returns correct data")]
    public async Task GetResource_Id2_ReturnsExpectedJson()
    {
        var fakeResponse = new RestResponse { StatusCode = OK, Content = JsonLoader.Load("GetResource_Id2.json") };
        _mockClient.Setup(c => c.GetResource(2)).ReturnsAsync(fakeResponse);

        var response = await _mockClient.Object.GetResources();
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
    [Order(3)]
    [Description("Unit Test → Simulated GET /unknown/23 returns 404")]
    public async Task GetResource_NotFound_Returns404()
    {
        var fakeResponse = new RestResponse { StatusCode = NotFound, Content = JsonLoader.Load("GetResource_NotFound.json") };
        _mockClient.Setup(c => c.GetInvalidResource()).ReturnsAsync(fakeResponse);

        var response = await _mockClient.Object.GetResources();

        Assert.That(response.StatusCode, Is.EqualTo(NotFound));
    }
}