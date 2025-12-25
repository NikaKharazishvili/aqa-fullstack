using OpenQA.Selenium;

namespace UiTests.Core;

public static class DriverManager
{
    static readonly AsyncLocal<IWebDriver> driver = null!;

    public static IWebDriver GetDriver() => driver.Value ?? throw new InvalidOperationException("Driver not initialized! Are you in a test?");

    public static void SetDriver(IWebDriver given) => driver.Value = given;
}