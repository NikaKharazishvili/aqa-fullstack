using MySql.Data.MySqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using static Shared.Utils;

namespace DbTests.Tests.RealMySQL;

/// <summary>
/// Real DB integration tests against an actual MySQL instance. Ignored by default for "clone & run".
/// To run: 1. Remove [Ignore]. 2. Update embedded appsettings.mysql.json. 3. Start local MySQL + run game_accounts.mysql.sql. 4. dotnet test --filter "Category=RealMySQL".
/// </summary>
[TestFixture(Ignore = "Requires local MySQL server")]
[Category(INTEGRATION)]
[Category(DB)]
[Category("RealMySQL")]
[Parallelizable(ParallelScope.Fixtures)]
public class DatabaseTest
{
    private MySqlConnection? connection;

    [OneTimeSetUp]
    public void Setup()
    {
        // NEW: Load embedded config
        string json = LoadEmbeddedText("Resources/appsettings.mysql.json");

        var config = new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(json)))
            .Build();

        connection = new MySqlConnection(config.GetConnectionString("DefaultConnection"));
        try
        {
            connection.Open();
            TestContext.Progress.WriteLine("Database connection established successfully");
            Console.WriteLine("Database connection established successfully");
        }
        catch (Exception ex) { Assert.Fail($"Database connection failed: {ex.Message}"); }
    }

    [OneTimeTearDown]
    public void Teardown() => connection?.Close();

    [Test]
    public void TestRecordCount() => ExecuteQueryAndAssert(
        "SELECT COUNT(*) FROM accounts",
        reader =>
        {
            Assert.That(reader.Read(), Is.True, "ResultSet should have at least one row");
            Assert.That(reader.GetInt32(0), Is.EqualTo(25), "Expected exactly 25 accounts");
        });

    [Test]
    public void TestNoDuplicateUsernames() => ExecuteQueryAndAssert(
        "SELECT username, COUNT(*) c FROM accounts GROUP BY username HAVING c > 1",
        reader => Assert.That(reader.HasRows, Is.False, "There should be no duplicate usernames"));

    [Test]
    public void TestNoDuplicateEmails() => ExecuteQueryAndAssert(
        "SELECT email, COUNT(*) c FROM accounts GROUP BY email HAVING c > 1",
        reader => Assert.That(reader.HasRows, Is.False, "There should be no duplicate emails"));

    [Test]
    public void TestNonNullConstraints() => ExecuteQueryAndAssert(
        "SELECT COUNT(*) FROM accounts WHERE username IS NULL OR password IS NULL OR email IS NULL",
        reader =>
        {
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetInt32(0), Is.EqualTo(0), "No columns should have NULL values");
        });

    [Test]
    public void TestSpecificUserData() => ExecuteQueryAndAssert(
        "SELECT username, password, email FROM accounts WHERE username = 'ShadowFang'",
        reader =>
        {
            Assert.That(reader.Read(), Is.True, "ShadowFang should exist");
            Assert.That(reader["username"], Is.EqualTo("ShadowFang"));
            Assert.That(reader["password"], Is.EqualTo("DragonSlayer"));
            Assert.That(reader["email"], Is.EqualTo("shadowfang@mail.com"));
        });

    private void ExecuteQueryAndAssert(string query, Action<MySqlDataReader> assertAction)
    {
        using var cmd = new MySqlCommand(query, connection);
        using var reader = cmd.ExecuteReader();
        assertAction(reader);
    }
}