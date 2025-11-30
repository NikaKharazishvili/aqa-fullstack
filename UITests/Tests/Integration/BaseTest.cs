using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using UiTests.Core;
using WebDriverManager.DriverConfigs.Impl;
using static Shared.Utils;

namespace UiTests.Tests;

/// <summary>Base class for all UI tests with driver setup and teardown.</summary>
[TestFixture]
[Category(INTEGRATION)]
[Category(UI)]
[Parallelizable(ParallelScope.Self)]
public abstract class BaseTest
{
    IWebDriver? _driver;

    [OneTimeSetUp]
    public void Setup()
    {
        var browser = ConfigReader.Get<string>("Browser").ToLowerInvariant();
        var headless = ConfigReader.Get<bool>("Headless");

        _driver = browser switch
        {
            "chrome" => CreateChrome(headless),
            "firefox" => CreateFirefox(headless),
            _ => throw new NotSupportedException($"Browser {browser} not supported")
        };

        DriverManager.SetDriver(_driver);
        if (!headless) _driver.Manage().Window.Maximize();
        _driver.Navigate().GoToUrl(ConfigReader.Get<string>("Url"));
    }

    [OneTimeTearDown]  // Quit driver after all tests are done
    public void TearDown()
    {
        _driver?.Quit();
        _driver?.Dispose();
        DriverManager.SetDriver(null);
    }

    IWebDriver CreateChrome(bool headless)
    {
        // Auto-downloads matching ChromeDriver for current Chrome version
        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());

        var options = new ChromeOptions();
        if (headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
        }
        return new ChromeDriver(options);
    }

    IWebDriver CreateFirefox(bool headless)
    {
        // Auto-downloads matching GeckoDriver for current Firefox version
        new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());

        var options = new FirefoxOptions();
        if (headless)
        {
            options.AddArgument("--headless");
            options.AddArgument("--width=1920");
            options.AddArgument("--height=1080");
        }
        return new FirefoxDriver(options);
    }
}