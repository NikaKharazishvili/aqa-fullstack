using System.Text.Json;

namespace UiTests.Core;

public static class ConfigReader
{
    static readonly JsonElement Config = JsonDocument.Parse(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "appsettings.json"))).RootElement;

    public static T Get<T>(string key) => Config.GetProperty(key).Deserialize<T>()!;
}