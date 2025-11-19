using Newtonsoft.Json.Linq;
using ApiTests.Clients;
using ApiTests.Models;
using static System.Net.HttpStatusCode;
using static Shared.Utils;

namespace ApiTests.Tests.Integration;

/// <summary>Integration tests verifying real API auth behavior.</summary>
[TestFixture]
[Category(INTEGRATION)]
[Category(API)]
[Parallelizable(ParallelScope.Fixtures)]
public class AuthTests
{
    [Test]
    [Description("POST /register → returns 200 and token for successful registration")]
    public async Task Register_ValidUser_ReturnsToken()
    {
        var authRequest = new AuthRequest
        {
            email = "eve.holt@reqres.in",
            password = "pistol"
        };

        using var client = new AuthClient();
        var response = await client.Register(authRequest);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(OK));

            var json = JObject.Parse(response.Content!);
            Assert.That(json["id"]?.Value<int>(), Is.EqualTo(4));
            Assert.That(json["token"]?.Value<string>(), Is.EqualTo("QpwL5tke4Pnpja7X4"));
        });
    }


    [Test]
    [Description("POST /register → returns 400 and error for missing password")]
    public async Task Register_MissingPassword_ReturnsError()
    {
        var authRequest = new AuthRequest
        {
            email = "sydney@fife"
        };

        using var client = new AuthClient();
        var response = await client.Register(authRequest);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(BadRequest));

            var json = JObject.Parse(response.Content!);
            Assert.That(json["error"]?.Value<string>(), Is.EqualTo("Missing password"));
        });
    }

    [Test]
    [Description("POST /login → returns 200 and token for successful login")]
    public async Task Login_ValidUser_ReturnsToken()
    {
        var authRequest = new AuthRequest
        {
            email = "eve.holt@reqres.in",
            password = "cityslicka"
        };

        using var client = new AuthClient();
        var response = await client.Login(authRequest);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(OK));

            var json = JObject.Parse(response.Content!);
            Assert.That(json["token"]?.Value<string>(), Is.EqualTo("QpwL5tke4Pnpja7X4"));
        });
    }

    [Test]
    [Description("POST /login → returns 400 and error for missing password")]
    public async Task Login_MissingPassword_ReturnsError()
    {
        var authRequest = new AuthRequest
        {
            email = "peter@klaven"
        };


        using var client = new AuthClient();
        var response = await client.Login(authRequest);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(BadRequest));

            var json = JObject.Parse(response.Content!);
            Assert.That(json["error"]?.Value<string>(), Is.EqualTo("Missing password"));
        });
    }
}