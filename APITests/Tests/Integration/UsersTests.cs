using ApiTests.Clients;
using Newtonsoft.Json.Linq;
using System.Net;  // To shortly write 'HttpStatusCode.OK'
using ApiTests.Models;
using static Shared.Utils;

namespace ApiTests.Tests.Integration;

[TestFixture]
[Category("Integration")]
public class UsersTests
{
    private IUsersClient _usersClient;

    [SetUp]
    public void Setup() => _usersClient = new UsersClient();

    [Test]
    [Order(1)]
    [Description("GET /users?page=2 → returns 200 and correct page data")]
    public async Task GetListUsers_Page2_Returns6()
    {
        var response = await _usersClient.GetUsers(2);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var json = JObject.Parse(response.Content!);
            Assert.That(json["page"]?.Value<int>(), Is.EqualTo(2), "Page should be 2");
            Assert.That(json["per_page"]?.Value<int>(), Is.EqualTo(6));
            Assert.That(json["total"]?.Value<int>(), Is.EqualTo(12));
            Assert.That(json["total_pages"]?.Value<int>(), Is.EqualTo(2));

            var data = json["data"] as JArray;
            Assert.That(data!.Count, Is.EqualTo(6));

            var firstUser = data[0] as JObject;
            Assert.That(firstUser?["id"]?.Value<int>(), Is.EqualTo(7));
            Assert.That(firstUser?["email"]?.Value<string>(), Is.EqualTo("michael.lawson@reqres.in"));
            Assert.That(firstUser?["avatar"]?.Value<string>(), Is.EqualTo("https://reqres.in/img/faces/7-image.jpg"));
        });
    }

    [Test]
    [Order(2)]
    [Description("GET /users/2 → returns 200 and correct user data")]
    public async Task GetUser_Id2_ReturnsJanet()
    {
        var response = await _usersClient.GetUser(2);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var json = JObject.Parse(response.Content!);

            var data = json["data"] as JObject;
            Assert.That(data?["id"]?.Value<int>(), Is.EqualTo(2));
            Assert.That(data?["email"]?.Value<string>(), Is.EqualTo("janet.weaver@reqres.in"));

            var support = json["support"] as JObject;
            Assert.That(support?["url"]?.Value<string>(), Is.EqualTo("https://contentcaddy.io?utm_source=reqres&utm_medium=json&utm_campaign=referral"));
            Assert.That(support?["text"]?.Value<string>(), Is.EqualTo("Tired of writing endless social media content? Let Content Caddy generate it for you."));
        });
    }

    [Test]
    [Order(3)]
    [Description("GET /users/23 → returns 404 Not Found")]
    public async Task GetUser_Id23_Returns404()
    {
        var response = await _usersClient.GetUser(23);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    [Order(4)]
    [Description("POST /users → creates a new user and returns 201 with correct data")]
    public async Task CreateUser_ValidData_Returns201()
    {
        var newUser = new UserRequest
        {
            name = "morpheus",
            job = "leader"
        };

        var response = await _usersClient.CreateUser(newUser);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var json = JObject.Parse(response.Content!);
            Assert.That(json["name"]?.Value<string>(), Is.EqualTo("morpheus"));
            Assert.That(json["job"]?.Value<string>(), Is.EqualTo("leader"));
            Assert.That(json["id"]?.Value<string>(), Is.Not.Null.And.Not.Empty);
            Assert.That(json["createdAt"]?.Value<DateTime>(), Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    [Order(5)]
    [Description("PUT /users/2 → updates user data and returns 200 with correct info")]
    public async Task UpdateUser_Id2_ValidData_Returns200()
    {
        var updatedUser = new UserRequest
        {
            name = "morpheus",
            job = "zion resident"
        };

        var response = await _usersClient.UpdateUser(2, updatedUser);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var json = JObject.Parse(response.Content!);
            Assert.That(json["name"]?.Value<string>(), Is.EqualTo("morpheus"));
            Assert.That(json["job"]?.Value<string>(), Is.EqualTo("zion resident"));
            Assert.That(json["updatedAt"]?.Value<DateTime>(), Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    [Order(5)]
    [Description("PATCH /users/2 → updates user data and returns 200 with correct info")]
    public async Task UpdateUser2_Id2_ValidData_Returns200()
    {
        var updatedUser = new UserRequest
        {
            name = "morpheus",
            job = "zion resident"
        };

        var response = await _usersClient.UpdateUser(2, updatedUser);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var json = JObject.Parse(response.Content!);
            Assert.That(json["name"]?.Value<string>(), Is.EqualTo("morpheus"));
            Assert.That(json["job"]?.Value<string>(), Is.EqualTo("zion resident"));
            Assert.That(json["updatedAt"]?.Value<DateTime>(), Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    [Order(6)]
    [Description("DELETE /users/2 → deletes user and returns 204 No Content")]
    public async Task DeleteUser_Id2_Returns204()
    {
        var response = await _usersClient.DeleteUser(2);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
}