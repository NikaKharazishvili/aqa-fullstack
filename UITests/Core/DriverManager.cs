using OpenQA.Selenium;

namespace UiTests.Core;

public static class DriverManager
{
    static readonly AsyncLocal<IWebDriver> _driver = new();

    public static IWebDriver GetDriver() => _driver.Value ?? throw new InvalidOperationException("Driver not initialized! Are you in a test?");

    public static void SetDriver(IWebDriver driver) => _driver.Value = driver;

    public static void QuitDriver() => _driver.Value?.Quit();
}