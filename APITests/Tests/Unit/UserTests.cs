using Moq;
using RestSharp;
using System.Net;
using ApiTests.Clients;
using Newtonsoft.Json.Linq;

namespace ApiTests.Tests.Unit;

[TestFixture]
[Category("Unit")]
public class UsersTests
{
    private Mock<IUsersClient> mockClient = null!;
    private IUsersClient _client => mockClient.Object;

    [SetUp]
    public void Setup() => mockClient = new Mock<IUsersClient>();

    [Test]
    [Order(1)]
    [Description("GET /users?page=2 → Moq returns 200 and fake page 2")]
    public async Task GetUsers_Page2_Returns200AndPage2()
    {
        // Arrange
        mockClient
            .Setup(x => x.GetUsers(2))
            .ReturnsAsync(new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = """
                {
                    "page": 2,
                    "per_page": 6,
                    "total": 12,
                    "total_pages": 2,
                    "data": [
                        {
                            "id": 7,
                            "email": "michael.lawson@reqres.in",
                            "first_name": "Michael"
                        }
                    ]
                }
                """
            });

        // Act
        var response = await _client.GetUsers(2);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Content, Is.Not.Null);

        var json = JObject.Parse(response.Content!);
        Assert.That(json["page"]?.Value<int>(), Is.EqualTo(2));
        Assert.That(json["data"]?[0]?["id"]?.Value<int>(), Is.EqualTo(7));
    }

    [Test]
    [Order(2)]
    [Description("GET /users?page=999 → Moq returns 404")]
    public async Task GetUsers_InvalidPage_Returns404()
    {
        mockClient
            .Setup(x => x.GetUsers(999))
            .ReturnsAsync(new RestResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = """{ "error": "Page not found" }"""
            });

        var response = await _client.GetUsers(999);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}