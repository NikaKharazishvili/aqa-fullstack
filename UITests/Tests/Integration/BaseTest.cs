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
    // This local driver instance ensures proper teardown per test fixture
    // DriverManager is still used for page objects, but relying solely on it was causing the last fixture teardown issue
    IWebDriver? driver;

    [OneTimeSetUp]
    public void Setup()
    {
        string browser = ConfigReader.Get<string>("Browser").ToLowerInvariant();
        bool headless = ConfigReader.Get<bool>("Headless");

        if (browser == "chrome")
        {
            var options = new ChromeOptions();
            if (headless) options.AddArguments("--headless=new", "--window-size=1920,1080");
            driver = new ChromeDriver(options);
        }
        else if (browser == "firefox")
        {
            var options = new FirefoxOptions();
            if (headless) options.AddArguments("--headless", "--width=1920", "--height=1080");
            driver = new FirefoxDriver(options);
        }
        else throw new NotSupportedException($"Browser {browser} not supported");

        DriverManager.SetDriver(driver);
        if (!headless) driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl(ConfigReader.Get<string>("Url"));
    }

    // Quit driver after all tests are done
    [OneTimeTearDown] public void TearDown() => driver?.Dispose();
}