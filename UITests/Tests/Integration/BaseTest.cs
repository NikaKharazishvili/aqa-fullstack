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
    IWebDriver? driver;
    string browser = ConfigReader.Get<string>("Browser").ToLowerInvariant();
    bool headless = ConfigReader.Get<bool>("Headless");

    [OneTimeSetUp]
    public void Setup()
    {

        driver = browser switch
        {
            "chrome" => CreateChrome(),
            "firefox" => CreateFirefox(),
            _ => throw new NotSupportedException($"Browser {browser} not supported")
        };

        DriverManager.SetDriver(driver);
        if (!headless) driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl(ConfigReader.Get<string>("Url"));
    }

    [OneTimeTearDown]  // Quit driver after all tests are done
    public void TearDown()
    {
        driver?.Quit();
        driver?.Dispose();
    }

    IWebDriver CreateChrome()
    {
        var options = new ChromeOptions();
        if (headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
        }
        return new ChromeDriver(options);
    }

    IWebDriver CreateFirefox()
    {
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