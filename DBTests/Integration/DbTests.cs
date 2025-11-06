using Microsoft.Data.Sqlite;
using static Shared.Utils;

namespace DbTests.Integration;

/// <summary>Integration tests for accounts table using a local in-memory SQLite database. Simplified for clone & run; no real DB server required.</summary>
[TestFixture]
[Category(INTEGRATION)]
[Category(DB)]
[Parallelizable(ParallelScope.All)]
public class DbTests
{
    SqliteConnection _connection = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        string sqlPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\TestData\game_accounts.sql");
        if (!File.Exists(sqlPath))
            Assert.Fail($"SQL file not found at: {sqlPath}");

        string script = File.ReadAllText(sqlPath);
        string[] commands = script.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var cmdText in commands)
        {
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = cmdText.Trim();
            if (!string.IsNullOrWhiteSpace(cmd.CommandText))
                cmd.ExecuteNonQuery();
        }
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _connection?.Close();
        _connection?.Dispose();
    }

    [Test]
    [Description("Check that the accounts table contains exactly 25 rows")]
    public void TestRecordCount() =>
    ExecuteQueryAndAssert("SELECT COUNT(*) FROM accounts",
        reader =>
        {
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetInt32(0), Is.EqualTo(25));
        });

    [Test]
    [Description("Verify that there are no duplicate usernames in accounts table")]
    public void TestNoDuplicateUsernames() =>
    ExecuteQueryAndAssert("SELECT username, COUNT(*) c FROM accounts GROUP BY username HAVING c > 1",
        reader => Assert.That(reader.HasRows, Is.False));

    [Test]
    [Description("Verify that there are no duplicate emails in accounts table")]
    public void TestNoDuplicateEmails() =>
    ExecuteQueryAndAssert("SELECT email, COUNT(*) c FROM accounts GROUP BY email HAVING c > 1",
        reader => Assert.That(reader.HasRows, Is.False));

    [Test]
    [Description("Check that username, password, and email columns do not contain NULL values")]
    public void TestNonNullConstraints() =>
    ExecuteQueryAndAssert("SELECT COUNT(*) FROM accounts WHERE username IS NULL OR password IS NULL OR email IS NULL",
        reader =>
        {
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.GetInt32(0), Is.EqualTo(0));
        });

    [Test]
    [Description("Verify that the user ShadowFang has the correct username, password, and email")]
    public void TestSpecificUserData() =>
    ExecuteQueryAndAssert("SELECT username, password, email FROM accounts WHERE username = 'ShadowFang'",
        reader =>
        {
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader["username"], Is.EqualTo("ShadowFang"));
            Assert.That(reader["password"], Is.EqualTo("DragonSlayer"));
            Assert.That(reader["email"], Is.EqualTo("shadowfang@mail.com"));
        });

    void ExecuteQueryAndAssert(string query, Action<SqliteDataReader> assertAction)
    {
        using var cmd = _connection.CreateCommand();
        cmd.CommandText = query;
        using var reader = cmd.ExecuteReader();
        assertAction(reader);
    }
}