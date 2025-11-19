using Moq;
using RestSharp;
using Newtonsoft.Json.Linq;
using ApiTests.Clients;
using ApiTests.Models;
using static System.Net.HttpStatusCode;
using static Shared.Utils;

namespace ApiTests.Tests.Unit;

/// <summary>Unit tests mocking user client CRUD and delay scenarios.</summary>
[TestFixture]
[Category(UNIT)]
[Category(API)]
[Parallelizable(ParallelScope.Fixtures)]
public class UsersTests
{
    private Mock<IUserClient> _mockClient;

    [SetUp] public void Setup() => _mockClient = new Mock<IUserClient>();

    [Test]
    [Description("Unit Test → Simulated GET /users?page=2 returns correct data")]
    public async Task GetListUsers_Page2_Returns6Users()
    {
        var fakeResponse = new RestResponse { StatusCode = OK, Content = JsonLoad("ListUsers_Page2.json") };
        _mockClient.Setup(c => c.GetUsers(2)).ReturnsAsync(fakeResponse);

        var response = await _mockClient.Object.GetUsers(2);
        var parsed = JObject.Parse(response.Content!);
        var data = parsed["data"] as JArray;

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(OK));
            Assert.That(parsed["page"]?.Value<int>(), Is.EqualTo(2));
            Assert.That(data!.Count, Is.EqualTo(6));
            Assert.That(data[0]?["email"]?.Value<string>(), Is.EqualTo("michael.lawson@reqres.in"));
        });
    }

    [Test]
    [Description("Unit Test → Simulated GET /users/2 returns Janet data")]
    public async Task GetUser_Id2_ReturnsJanet()
    {
        var fakeResponse = new RestResponse { StatusCode = OK, Content = JsonLoad("GetUser_Id2.json") };
        _mockClient.Setup(c => c.GetUser(2)).ReturnsAsync(fakeResponse);

        var response = await _mockClient.Object.GetUser(2);
        var parsed = JObject.Parse(response.Content!);
        var data = parsed["data"] as JObject;

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(OK));
            Assert.That(data?["email"]?.Value<string>(), Is.EqualTo("janet.weaver@reqres.in"));
        });
    }

    [Test]
    [Description("Unit Test → Simulated GET /users/23 returns 404")]
    public async Task GetUser_NotFound_Returns404()
    {
        var fakeResponse = new RestResponse { StatusCode = NotFound, Content = JsonLoad("GetUser_NotFound.json") };
        _mockClient.Setup(c => c.GetUser(23)).ReturnsAsync(fakeResponse);

        var response = await _mockClient.Object.GetUser(23);

        Assert.That(response.StatusCode, Is.EqualTo(NotFound));
    }

    [Test]
    [Description("Unit Test → Simulated POST /users returns 201")]
    public async Task CreateUser_ValidData_Returns201()
    {
        var fakeResponse = new RestResponse { StatusCode = Created, Content = JsonLoad("CreateUser_Success.json") };
        _mockClient.Setup(c => c.CreateUser(It.IsAny<UserRequest>())).ReturnsAsync(fakeResponse);

        var newUser = new UserRequest { name = "morpheus", job = "leader" };

        var response = await _mockClient.Object.CreateUser(newUser);
        var parsed = JObject.Parse(response.Content!);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(Created));
            Assert.That(parsed["name"]?.Value<string>(), Is.EqualTo("morpheus"));
            Assert.That(parsed["job"]?.Value<string>(), Is.EqualTo("leader"));
        });
    }

    [Test]
    [Description("Unit Test → Simulated PUT /users/2 returns 200")]
    public async Task UpdateUser_Id2_Returns200()
    {
        var fakeResponse = new RestResponse { StatusCode = OK, Content = JsonLoad("UpdateUser_Success.json") };
        _mockClient.Setup(c => c.UpdateUser(2, It.IsAny<UserRequest>())).ReturnsAsync(fakeResponse);

        var updatedUser = new UserRequest { name = "morpheus", job = "zion resident" };

        var response = await _mockClient.Object.UpdateUser(2, updatedUser);
        var parsed = JObject.Parse(response.Content!);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(OK));
            Assert.That(parsed["job"]?.Value<string>(), Is.EqualTo("zion resident"));
        });
    }

    [Test]
    [Description("Unit Test → Simulated PATCH /users/2 returns 200 with partial update")]
    public async Task PatchUser_Id2_Partial_Returns200()
    {
        var fakeResponse = new RestResponse { StatusCode = OK, Content = JsonLoad("UpdateUser_Success.json") };
        _mockClient.Setup(c => c.PatchUser(2, It.IsAny<UserRequest>())).ReturnsAsync(fakeResponse);
        var patch = new UserRequest { job = "zion resident" };  // Partial
        var response = await _mockClient.Object.PatchUser(2, patch);
        var parsed = JObject.Parse(response.Content!);
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(OK));
            Assert.That(parsed["job"]?.Value<string>(), Is.EqualTo("zion resident"));
        });
    }

    [Test]
    [Description("Unit Test → Simulated DELETE /users/2 returns 204")]
    public async Task DeleteUser_Id2_Returns204()
    {
        var fakeResponse = new RestResponse { StatusCode = NoContent, Content = JsonLoad("DeleteUser_Success.json") };
        _mockClient.Setup(c => c.DeleteUser(2)).ReturnsAsync(fakeResponse);

        var response = await _mockClient.Object.DeleteUser(2);

        Assert.That(response.StatusCode, Is.EqualTo(NoContent));
    }

    [Test]
    [Description("Unit Test → Simulated GET /users?delay=2 returns 200")]
    public async Task DelayedResponse_Returns200()
    {
        var fakeResponse = new RestResponse { StatusCode = OK, Content = JsonLoad("ListUsers_Page1_Delayed.json") };
        _mockClient.Setup(c => c.DelayedResponse(2)).ReturnsAsync(fakeResponse);

        var response = await _mockClient.Object.DelayedResponse(2);

        Assert.That(response.StatusCode, Is.EqualTo(OK));
    }
}