namespace ApiTests.Unit.Helpers;

/// <summary>Loads JSON test data from files for unit test mocking.</summary>
public static class JsonLoader
{
    public static string Load(string fileName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Unit", "TestData", fileName);
        if (!File.Exists(path)) throw new FileNotFoundException($"JSON not found: {path}");

        return File.ReadAllText(path);
    }
}