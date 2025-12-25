using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using UiTests.Core;

namespace UiTests.Pages;

/// <summary>Base class for all Page Objects with shared utilities like waits and alerts.</summary>
public class BasePage
{
    protected IWebDriver Driver => DriverManager.GetDriver();
    protected WebDriverWait Wait { get; }
    protected Actions Actions { get; }

    public BasePage()
    {
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        Actions = new Actions(Driver);
    }

    public IWebElement Find(string css) => Wait.Until(_ =>
    {
        try
        {
            var element = Driver.FindElement(By.CssSelector(css));
            return (element.Displayed && element.Enabled) ? element : null;
        }
        catch (NoSuchElementException) { return null; }
        catch (StaleElementReferenceException) { return null; }
    });

    public List<IWebElement> FindMany(string css) => Driver.FindElements(By.CssSelector(css)).ToList();

    public void HoverAndClick(IWebElement element)
    {
        try { element.Click(); }
        catch (ElementClickInterceptedException) { ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", element); }
    }

    public IAlert GetAlert() => Wait.Until(_ =>
    {
        try { return Driver.SwitchTo().Alert(); }
        catch (NoAlertPresentException) { return null; }
    });

    public string GetHeaderText() => Find("h1[itemprop='headline']").Text; // Check header of the current page to verify we are on the right page

    public void WaitForTextToBe(IWebElement element, string text) => Wait.Until(_ => element.Text.Contains(text));
    public void WaitForElementVisible(IWebElement element) => Wait.Until(_ => element.Displayed);
    public void WaitForElementNotVisible(IWebElement element) => Wait.Until(_ => !element.Displayed);
}