using NUnit.Framework;

namespace Shared;

public static class Utils
{
    // Uses TestContext.WriteLine instead of Console.WriteLine to ensure logs appear in test reports and CI/CD pipelines
    // Simple log method, similar to Python's print(). Allows usage like: Log("Hello", "World") → prints "Hello World"
    // We use 'params' so we can write: Log("a", 1, true) instead of: Log(new object[] { "a", 1, true })
    public static void Log(params object[] objects) => TestContext.WriteLine(string.Join(" ", objects));

    // Pause for 'seconds'
    public static void Wait(float seconds) => Thread.Sleep((int)(seconds * 1000));

    // Test categories for filtering, e.g.: dotnet test --filter "Category=Integration"
    public const string API = "Api";
    public const string DB = "Db";
    public const string UI = "Ui";
    public const string INTEGRATION = "Integration";
    public const string UNIT = "Unit";
    public const string SMOKE = "Smoke";
}