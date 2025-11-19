using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using UiTests.Core;
using static Shared.Utils;

namespace UiTests.Pages;

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
    public IWebElement FindVisible(string css) => Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(css)));
    public List<IWebElement> FindMany(string css) => Driver.FindElements(By.CssSelector(css)).ToList();

    protected void HoverAndClick(IWebElement element)
    {
        ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center', inline: 'center'});", element);  // JS scroll = 100% reliable
        Wait(0.2f);  // Small delay for Firefox (Firefox sometimes ignores instant hover)
        // Actions.MoveToElement(element).Click().Perform();
        element.Click();
    }

    public string GetHeaderText() => Find("h1[itemprop='headline']").Text;  // Check header of the current page to verify we are on the right page

    public void AcceptAlert()
    {
        Wait.Until(ExpectedConditions.AlertIsPresent());
        Driver.SwitchTo().Alert().Accept();
    }
}