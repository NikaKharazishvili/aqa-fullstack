using ApiTests.Clients;
using Newtonsoft.Json.Linq;
using static Shared.Utils;

namespace ApiTests.Tests.Integration;

[TestFixture]
[Category("Integration")]  // CI/CD filter
public class UsersTests
{
    private IUsersClient usersClient = null!;

    [SetUp]
    public void Setup() => usersClient = new UsersClient();

    [Test]
    [Order(1)]
    [Description("GET /users?page=2 → returns 200 and correct page data")]
    public async Task GetUsers_Page2_ReturnsExpectedData()
    {
        // Act
        var response = await usersClient.GetUsers(2);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

        var json = JObject.Parse(response.Content!);
        Log("GET /users?page=2 → 200 OK");

        Assert.Multiple(() =>
        {
            Assert.That(json["page"]?.Value<int>(), Is.EqualTo(2), "Page should be 2");
            Assert.That(json["per_page"]?.Value<int>(), Is.EqualTo(6));
            Assert.That(json["total"]?.Value<int>(), Is.EqualTo(12));
            Assert.That(json["total_pages"]?.Value<int>(), Is.EqualTo(2));

            var data = json["data"] as JArray;
            Assert.That(data!.Count, Is.EqualTo(6));

            var firstUser = data[0] as JObject;
            Assert.That(firstUser?["id"]?.Value<int>(), Is.EqualTo(7));
            Assert.That(firstUser?["email"]?.Value<string>(), Is.EqualTo("michael.lawson@reqres.in"));
            Assert.That(firstUser?["first_name"]?.Value<string>(), Is.EqualTo("Michael"));
        });

        Log("All assertions passed");
    }
}