using OpenQA.Selenium;

namespace UiTests.Core;

public static class DriverManager
{
    static readonly AsyncLocal<IWebDriver> driver = new ();

    public static IWebDriver GetDriver() => driver.Value ?? throw new InvalidOperationException("Driver not initialized!");

    public static void SetDriver(IWebDriver given) => driver.Value = given;
}