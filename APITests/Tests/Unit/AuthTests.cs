using Moq;
using RestSharp;
using Newtonsoft.Json.Linq;
using ApiTests.Clients;
using ApiTests.Models;
using static System.Net.HttpStatusCode;
using static Shared.Utils;

namespace ApiTests.Tests.Unit;

/// <summary>Unit tests mocking auth client behavior with fake responses.</summary>
[TestFixture]
[Category(UNIT)]
[Category(API)]
[Parallelizable(ParallelScope.Fixtures)]
public class AuthTests
{
    private Mock<IAuthClient> _mockClient;

    [SetUp] public void Setup() => _mockClient = new Mock<IAuthClient>();

    [Test]
    [Description("Unit Test → Simulated POST /register returns 200 with token")]
    public async Task Register_ValidUser_ReturnsToken()
    {
        var request = new AuthRequest { email = "eve.holt@reqres.in", password = "pistol" };
        var fakeResponse = new RestResponse { StatusCode = OK, Content = LoadEmbeddedText("Tests/Unit/TestData/Register_Successful.json"), };
        _mockClient
            .Setup(c => c.Register(It.Is<AuthRequest>(r => r.email == request.email && r.password == request.password)))
            .ReturnsAsync(fakeResponse);

        var response = await _mockClient.Object.Register(request);
        var parsed = JObject.Parse(response.Content!);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(OK));
            Assert.That(parsed["id"]?.Value<int>(), Is.EqualTo(4));
            Assert.That(parsed["token"]?.Value<string>(), Is.EqualTo("QpwL5tke4Pnpja7X4"));
        });
    }

    [Test]
    [Description("Unit Test → Simulated POST /register with missing password returns 400")]
    public async Task Register_MissingPassword_ReturnsError()
    {
        var request = new AuthRequest { email = "sydney@fife" };
        var fakeResponse = new RestResponse { StatusCode = BadRequest, Content = LoadEmbeddedText("Tests/Unit/TestData/Register_Unsuccessful.json"), };
        _mockClient
            .Setup(c => c.Register(It.Is<AuthRequest>(r => r.email == request.email && string.IsNullOrEmpty(r.password))))
            .ReturnsAsync(fakeResponse);

        var response = await _mockClient.Object.Register(request);
        var parsed = JObject.Parse(response.Content!);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(BadRequest));
            Assert.That(parsed["error"]?.Value<string>(), Is.EqualTo("Missing password"));
        });
    }

    [Test]
    [Description("Unit Test → Simulated POST /login returns 200 with token")]
    public async Task Login_ValidUser_ReturnsToken()
    {
        var request = new AuthRequest { email = "eve.holt@reqres.in", password = "cityslicka" };
        var fakeResponse = new RestResponse { StatusCode = OK, Content = LoadEmbeddedText("Tests/Unit/TestData/Register_Successful.json"), };
        _mockClient
            .Setup(c => c.Login(It.Is<AuthRequest>(r => r.email == request.email && r.password == request.password)))
            .ReturnsAsync(fakeResponse);

        var response = await _mockClient.Object.Login(request);
        var parsed = JObject.Parse(response.Content!);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(OK));
            Assert.That(parsed["token"]?.Value<string>(), Is.EqualTo("QpwL5tke4Pnpja7X4"));
        });
    }

    [Test]
    [Description("Unit Test → Simulated POST /login with missing password returns 400")]
    public async Task Login_MissingPassword_ReturnsError()
    {
        var request = new AuthRequest { email = "peter@klaven" };
        var fakeResponse = new RestResponse { StatusCode = BadRequest, Content = LoadEmbeddedText("Tests/Unit/TestData/Login_Unsuccessful.json"), };
        _mockClient
            .Setup(c => c.Login(It.Is<AuthRequest>(r => r.email == request.email && string.IsNullOrEmpty(r.password))))
            .ReturnsAsync(fakeResponse);

        var response = await _mockClient.Object.Login(request);
        var parsed = JObject.Parse(response.Content!);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(BadRequest));
            Assert.That(parsed["error"]?.Value<string>(), Is.EqualTo("Missing password"));
        });
    }
}
