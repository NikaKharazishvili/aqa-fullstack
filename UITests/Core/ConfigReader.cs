using System.Text.Json;
using static Shared.Utils;

namespace UiTests.Core;

public static class ConfigReader
{
    static readonly JsonElement Config;

    static ConfigReader()
    {
        string json = LoadEmbeddedText("appsettings.json");
        Config = JsonDocument.Parse(json).RootElement;
    }

    public static T Get<T>(string key) => Config.GetProperty(key).Deserialize<T>()!;
}