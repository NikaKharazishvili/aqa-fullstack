using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using UiTests.Core;

namespace UiTests.Pages;

public abstract class BasePage
{
    protected IWebDriver Driver => DriverManager.GetDriver();

    protected WebDriverWait Wait => new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
    protected IWebElement Find(string css) => Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(css)));
    protected IWebElement FindVisible(string css) => Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(css)));
    
    public string GetHeaderText() => Find("h1[itemprop='headline']").Text;
    public void GoToHomePage() => Find(".attachment-full.size-full").Click();
}