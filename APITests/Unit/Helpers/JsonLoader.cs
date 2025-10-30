namespace ApiTests.Unit.Helpers;

public static class JsonLoader
{
    public static string Load(string fileName)
    {
        var projectRoot = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..");
        var path = Path.Combine(projectRoot, "Unit", "TestData", fileName);
        path = Path.GetFullPath(path);

        if (!File.Exists(path)) throw new FileNotFoundException($"JSON test file not found: {path}");
        return File.ReadAllText(path);
    }
}