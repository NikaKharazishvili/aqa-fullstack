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

    public IWebElement Find(string css) =>
    Wait.Until(driver =>
    {
        try
        {
            var e = driver.FindElement(By.CssSelector(css));
            return (e.Displayed && e.Enabled) ? e : null;
        }
        catch (NoSuchElementException) { return null; }
        catch (StaleElementReferenceException) { return null; }
    });

    public List<IWebElement> FindMany(string css) => Driver.FindElements(By.CssSelector(css)).ToList();

    public void HoverAndClick(IWebElement e)
    {
        try { e.Click(); }
        catch (ElementClickInterceptedException) { ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", e); }
    }

    public IAlert GetAlert() =>
    Wait.Until(driver =>
    {
        try { return driver.SwitchTo().Alert(); }
        catch (NoAlertPresentException) { return null; }
    });

    public string GetHeaderText() => Find("h1[itemprop='headline']").Text; // Check header of the current page to verify we are on the right page

    public void WaitForTextToBe(IWebElement e, string text) => Wait.Until(_ => e.Text.Contains(text));
    public void WaitForElementVisible(IWebElement e) => Wait.Until(_ => e.Displayed);
    public void WaitForElementNotVisible(IWebElement e) => Wait.Until(_ => !e.Displayed);
}