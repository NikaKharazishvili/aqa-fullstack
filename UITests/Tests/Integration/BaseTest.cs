using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using UiTests.Core;
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
    string _browser = ConfigReader.Get<string>("Browser").ToLowerInvariant();
    bool _headless = ConfigReader.Get<bool>("Headless");

    [OneTimeSetUp]
    public void Setup()
    {

        _driver = _browser switch
        {
            "chrome" => CreateChrome(),
            "firefox" => CreateFirefox(),
            _ => throw new NotSupportedException($"Browser {_browser} not supported")
        };

        DriverManager.SetDriver(_driver);
        if (!_headless) _driver.Manage().Window.Maximize();
        _driver.Navigate().GoToUrl(ConfigReader.Get<string>("Url"));
    }

    [OneTimeTearDown]  // Quit driver after all tests are done
    public void TearDown()
    {
        _driver?.Quit();
        _driver?.Dispose();
        DriverManager.SetDriver(null);
    }

    IWebDriver CreateChrome()
    {
        var options = new ChromeOptions();
        if (_headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
        }
        return new ChromeDriver(options);
    }

    IWebDriver CreateFirefox()
    {
        var options = new FirefoxOptions();
        if (_headless)
        {
            options.AddArgument("--headless");
            options.AddArgument("--width=1920");
            options.AddArgument("--height=1080");
        }
        return new FirefoxDriver(options);
    }
}