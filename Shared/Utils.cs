using System.Reflection;

namespace Shared;

/// <summary>Common utilities for all tests (API, UI, etc.)</summary>
public static class Utils
{
    // Test categories for filtering, e.g. dotnet test --filter "Category=Integration"
    public const string API = "Api";
    public const string DB = "Db";
    public const string UI = "Ui";
    public const string INTEGRATION = "Integration";
    public const string UNIT = "Unit";
    public const string SMOKE = "Smoke";

    // Pause for 'seconds'
    public static void Wait(float seconds) => Thread.Sleep((int)(seconds * 1000));

    // Loads JSON test data from files for Api Unit test mocking
    public static string JsonLoad(string fileName)
    {
        var assembly = Assembly.GetCallingAssembly();  // The assembly of the project that *called* this method (e.g. ApiTests.dll)
        var resourceName = $"ApiTests.Tests.Unit.TestData.{fileName}";  // Full embedded resource path
        using var stream = assembly.GetManifestResourceStream(resourceName) ?? throw new FileNotFoundException($"Embedded JSON not found: {resourceName}");  // Throw error if not found
        return new StreamReader(stream).ReadToEnd();
    }
}