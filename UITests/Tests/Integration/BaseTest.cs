using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using UiTests.Core;
using static Shared.Utils;

namespace UiTests.Tests;

[TestFixture]
[Category(INTEGRATION)]
[Category(UI)]
[Parallelizable(ParallelScope.Fixtures)]
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
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl(ConfigReader.Get<string>("Url"));
    }

    [OneTimeTearDown]  // Quit driver after all tests are done
    public void TearDown() => DriverManager.QuitDriver();

    IWebDriver CreateChrome(bool headless)
    {
        var options = new ChromeOptions();
        if (headless) options.AddArgument("--headless=new");
        return new ChromeDriver(options);
    }

    IWebDriver CreateFirefox(bool headless)
    {
        var options = new FirefoxOptions();
        if (headless) options.AddArgument("--headless");
        return new FirefoxDriver(options);
    }
}