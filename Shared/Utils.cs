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

    // Loads an embedded resource file as a string. Used by tests to load config files, SQL scripts, or JSON test data
    public static string LoadEmbeddedText(string resourcePath)
    {
        var assembly = Assembly.GetCallingAssembly();
        var resourceName = $"{assembly.GetName().Name}.{resourcePath.Replace('/', '.').Replace('\\', '.')}";
        var stream = assembly.GetManifestResourceStream(resourceName) ?? throw new FileNotFoundException($"Missing embedded resource: {resourceName}");
        return new StreamReader(stream).ReadToEnd();
    }
}