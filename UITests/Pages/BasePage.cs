using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
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

    public IWebElement Find(string css) => Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(css)));
    public List<IWebElement> FindMany(string css) => Driver.FindElements(By.CssSelector(css)).ToList();

    protected void HoverAndClick(IWebElement element)
    {
        try { element.Click(); }
        catch (ElementClickInterceptedException) { ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", element); }
    }

    public string GetHeaderText() => Find("h1[itemprop='headline']").Text;  // Check header of the current page to verify we are on the right page

    public void WaitForTextToBe(IWebElement element, string text) => Wait.Until(ExpectedConditions.TextToBePresentInElement(element, text));

    public void WaitForElementNotVisible(IWebElement element) =>
        Wait.Until(driver =>
        {
            try { return !element.Displayed; }
            catch (StaleElementReferenceException) { return true; }
            catch (NoSuchElementException) { return true; }
        });

    public void WaitForElementVisible(IWebElement element) =>
        Wait.Until(driver =>
        {
            try { return element.Displayed; }
            catch (StaleElementReferenceException) { return false; }
            catch (NoSuchElementException) { return false; }
        });

    public IAlert GetAlert()
    {
        Wait.Until(ExpectedConditions.AlertIsPresent());
        return Driver.SwitchTo().Alert();
    }
}