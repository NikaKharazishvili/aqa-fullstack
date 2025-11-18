using System.Text.Json;

namespace UiTests.Core;

public static class ConfigReader
{
    static readonly JsonDocument Config = JsonDocument.Parse(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "appsettings.json")));

    public static string Get(string key) => Config.RootElement.GetProperty(key).GetString()!;
}