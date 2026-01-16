using MySql.Data.MySqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using static Shared.Utils;

namespace DbTests.Tests.RealMySQL;

/// <summary>
/// Real DB integration tests against an actual MySQL instance. Ignored by default for "clone & run".
/// To run: 1. Remove [Ignore]. 2. Update embedded appsettings.mysql.json as needed. 3. Start local MySQL + run game_accounts.mysql.sql. 4. dotnet test --filter "Category=RealMySQL".
/// </summary>
[TestFixture]
[Ignore("Requires a real local MySQL server to test")]
[Category(INTEGRATION)]
[Category(DB)]
[Parallelizable(ParallelScope.Fixtures)]
public class DbTests
{
    MySqlConnection connection = null!;

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
            Assert.Pass("Database connection established successfully");
        }
        catch (Exception ex) { Assert.Fail($"Database connection failed: {ex.Message}"); }
    }

    [OneTimeTearDown] public void TearDown() => connection.Close();

    // Helper to execute query and run assertions on the reader
    void ExecuteQueryAndAssert(string query, Action<MySqlDataReader> assertAction)
    {
        using var cmd = new MySqlCommand(query, connection);
        using var reader = cmd.ExecuteReader();
        assertAction(reader);
    }

    [Test, Description("Check that the accounts table contains exactly 25 rows")]
    public void TestRecordCount() => ExecuteQueryAndAssert("SELECT COUNT(*) FROM accounts", reader =>
    {
        Assert.That(reader.Read(), "ResultSet should have at least one row"); // Read() moves to next row. Returns true if one exists
        Assert.That(reader.GetInt32(0) == 25, "Expected exactly 25 accounts"); // GetInt32(0) reads first column as int
    });

    [Test, Description("Verify that there are no duplicate usernames in accounts table")]
    public void TestNoDuplicateUsernames() => ExecuteQueryAndAssert("SELECT username, COUNT(*) c FROM accounts GROUP BY username HAVING c > 1", reader =>
    Assert.That(!reader.HasRows, "There should be no duplicate usernames")); // !reader.HasRows means no duplicates found

    [Test, Description("Verify that there are no duplicate emails in accounts table")]
    public void TestNoDuplicateEmails() => ExecuteQueryAndAssert("SELECT email, COUNT(*) c FROM accounts GROUP BY email HAVING c > 1", reader =>
    Assert.That(!reader.HasRows, "There should be no duplicate emails"));

    [Test, Description("Check that username, password, and email columns do not contain NULL values")]
    public void TestNonNullConstraints() => ExecuteQueryAndAssert("SELECT COUNT(*) FROM accounts WHERE username IS NULL OR password IS NULL OR email IS NULL", reader =>
    {
        Assert.That(reader.Read(), "ResultSet should have at least one row");
        Assert.That(reader.GetInt32(0) == 0, "No columns should have NULL values");
    });

    [Test, Description("Verify that the user ShadowFang has the correct username, password, and email")]
    public void TestSpecificUserData() => ExecuteQueryAndAssert("SELECT username, password, email FROM accounts WHERE username = 'ShadowFang'", reader =>
    {
        Assert.That(reader.Read(), "ShadowFang should exist");
        Assert.That(reader["username"], Is.EqualTo("ShadowFang")); // Object value of column 'username'
        Assert.That(reader["password"], Is.EqualTo("DragonSlayer"));
        Assert.That(reader["email"], Is.EqualTo("shadowfang@mail.com"));
    });
}