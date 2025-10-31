namespace ApiTests.Unit.Helpers;

public static class JsonLoader
{
    public static string Load(string fileName)
    {
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..", "Unit", "TestData", fileName);
        if (!File.Exists(path)) throw new FileNotFoundException($"JSON not found: {path}");

        return File.ReadAllText(path);
    }
}