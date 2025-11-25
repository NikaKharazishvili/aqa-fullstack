using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using UiTests.Core;
using WebDriverManager.DriverConfigs.Impl;
using static Shared.Utils;

namespace UiTests.Tests;

[TestFixture]
[Category(INTEGRATION)]
[Category(UI)]
[Parallelizable(ParallelScope.Self)]
public abstract class BaseTest
{
    [OneTimeSetUp]
    public void Setup()
    {
        var browser = ConfigReader.Get<string>("Browser").ToLowerInvariant();
        var headless = ConfigReader.Get<bool>("Headless");

        var driver = browser switch
        {
            "chrome" => CreateChrome(headless),
            "firefox" => CreateFirefox(headless),
            _ => throw new NotSupportedException($"Browser {browser} not supported")
        };

        DriverManager.SetDriver(driver);
        if (!headless) driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl(ConfigReader.Get<string>("Url"));
    }

    [OneTimeTearDown]  // Quit driver after all tests are done
    public void TearDown() => DriverManager.QuitDriver();

    IWebDriver CreateChrome(bool headless)
    {
        // Auto-downloads matching ChromeDriver to avoid version conflicts. Ensures tests run on any PC/CI environment without manual driver setup
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
        // Auto-downloads matching ChromeDriver to avoid version conflicts. Ensures tests run on any PC/CI environment without manual driver setup
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