using OpenQA.Selenium;

namespace UiTests.Pages;

/// <summary>Page Object for the Popups page. Provides methods to navigate the page and interact with alert, confirm, and prompt popups.</summary>
public class PopupsPage : BasePage
{
    IWebElement PopupsLink => Find("a[href*='popups']");
    IWebElement AlertPopup => Find("#alert");
    IWebElement ConfirmPopup => Find("#confirm");
    IWebElement PromptPopup => Find("#prompt");
    IWebElement ConfirmPopupText => Find("#confirmResult");
    IWebElement PromptPopupText => Find("#promptResult");

    public void GoToPopupsPage() => HoverAndClick(PopupsLink);

    public void ClickAlertPopup() => AlertPopup.Click();

    public void ClickConfirmPopup() => ConfirmPopup.Click();

    public string GetConfirmPopupText() => ConfirmPopupText.Text;

    public void ClickPromptPopup() => PromptPopup.Click();

    public string GetPromptPopupText() => PromptPopupText.Text;
}