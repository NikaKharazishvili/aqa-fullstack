using Newtonsoft.Json.Linq;
using ApiTests.Clients;
using static System.Net.HttpStatusCode;
using static Shared.Utils;

namespace ApiTests.Tests.Integration;

[TestFixture]
[Category(Integr)]
[Category(Api)]
[Parallelizable(ParallelScope.All)]
public class ResourceTests
{
    private IResourceClient _resourceClient;

    [SetUp] public void Setup() => _resourceClient = new ResourceClient();

    [Test]
    [Order(1)]
    [Description("GET /unknown → returns 200 and correct resource data")]
    public async Task GetListResources_Returns6Resources()
    {
        var response = await _resourceClient.GetResources();

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(OK));

            var json = JObject.Parse(response.Content!);
            Assert.That(json["page"]?.Value<int>(), Is.EqualTo(1), "Page should be 1");
            Assert.That(json["per_page"]?.Value<int>(), Is.EqualTo(6));
            Assert.That(json["total"]?.Value<int>(), Is.EqualTo(12));
            Assert.That(json["total_pages"]?.Value<int>(), Is.EqualTo(2));

            var data = json["data"] as JArray;
            Assert.That(data!.Count, Is.EqualTo(6));

            var firstResource = data[0] as JObject;
            Assert.That(firstResource?["id"]?.Value<int>(), Is.EqualTo(1));
            Assert.That(firstResource?["name"]?.Value<string>(), Is.EqualTo("cerulean"));
            Assert.That(firstResource?["year"]?.Value<int>(), Is.EqualTo(2000));
            Assert.That(firstResource?["pantone_value"]?.Value<string>(), Is.EqualTo("15-4020"));
        });
    }

    [Test]
    [Order(2)]
    [Description("GET /unknown/2 → returns 200 and correct resource data")]
    public async Task GetResource_Id2_ReturnsCorrectResource()
    {
        var response = await _resourceClient.GetResource(2);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(OK));

            var json = JObject.Parse(response.Content!);
            var data = json["data"] as JObject;

            Assert.That(data?["id"]?.Value<int>(), Is.EqualTo(2));
            Assert.That(data?["name"]?.Value<string>(), Is.EqualTo("fuchsia rose"));
            Assert.That(data?["year"]?.Value<int>(), Is.EqualTo(2001));
            Assert.That(data?["pantone_value"]?.Value<string>(), Is.EqualTo("17-2031"));
        });
    }

    [Test]
    [Order(3)]
    [Description("GET /unknown/23 → returns 404")]
    public async Task GetResource_Id23_Returns404()
    {
        var response = await _resourceClient.GetResource(23);

        Assert.That(response.StatusCode, Is.EqualTo(NotFound));
    }
}