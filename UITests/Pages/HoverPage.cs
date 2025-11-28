using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace UiTests.Pages;

/// <summary>Page Object for the Hover page.Handles navigation and hover interaction for verifying dynamic text changes.</summary>
public class HoverPage : BasePage
{
    IWebElement HoverLink => Find("a[href*='hover/']");
    IWebElement ElementToHover => Find("#mouse_over");

    public void GoToHoverPage() => HoverAndClick(HoverLink);

    public String GetHoverText() => ElementToHover.Text;

    public void HoverOverElement() => Actions.MoveToElement(ElementToHover).Perform();
}