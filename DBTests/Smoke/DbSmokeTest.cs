using Microsoft.Data.Sqlite;
using static Shared.Utils;

namespace DbTests.Smoke;

/// <summary>Smoke test for accounts table. Ensures local in-memory SQL file runs correctly. Clone & run friendly.</summary>
[TestFixture]
[Category(SMOKE)]
[Category(DB)]
[Parallelizable(ParallelScope.All)]
public class DbSmokeTest
{
    private SqliteConnection _connection = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        string sqlPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\TestData\game_accounts.sql");
        if (!File.Exists(sqlPath))
            Assert.Fail($"SQL file not found at: {sqlPath}");

        string script = File.ReadAllText(sqlPath);
        foreach (var cmdText in script.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
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
    [Description("Smoke test: verify DB loads and accounts table has data")]
    public void AccountsTableSmokeTest() =>
        ExecuteQueryAndAssert("SELECT COUNT(*) FROM accounts", reader =>
        {
            Assert.That(reader.Read(), Is.True, "Accounts table should have rows");
            Assert.That(reader.GetInt32(0), Is.GreaterThan(0), "Accounts table should not be empty");
        });

    void ExecuteQueryAndAssert(string query, Action<SqliteDataReader> assertAction)
    {
        using var cmd = _connection.CreateCommand();
        cmd.CommandText = query;
        using var reader = cmd.ExecuteReader();
        assertAction(reader);
    }
}