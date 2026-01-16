using OpenQA.Selenium;

namespace UiTests.Pages;

/// <summary>Page Object for the Modals page. Provides methods to interact with modals.</summary>
public class ModalsPage : BasePage
{
    IWebElement ModalsLink => Find("a[href*='modals/']");
    IWebElement SimpleModal => Find("#simpleModal");
    IWebElement SimpleModalText => Find("#pum_popup_title_1318");
    IWebElement SimpleModalCloseButton => Find("#popmake-1318 .pum-close.popmake-close");

    IWebElement FormModal => Find("#formModal");
    IWebElement FormModalText => Find("#pum_popup_title_674");
    IWebElement FormModalNameInput => Find("#g1051-name");
    IWebElement FormModalEmailInput => Find("#g1051-email");
    IWebElement FormModalMessageInput => Find("#contact-form-comment-g1051-message");
    IWebElement FormModalSubmitButton => Find(".pushbutton-wide");

    public void GoToModalsPage() => HoverAndClick(ModalsLink);

    public string OpenSimpleModalAndGetTextThenClose()
    {
        SimpleModal.Click();
        WaitForTextToBe(SimpleModalText, "Simple Modal");
        string text = SimpleModalText.Text; // Remember text before closing modal, so we will be able to return it
        SimpleModalCloseButton.Click();
        return text;
    }

    public string OpenFormModalAndGetText()
    {
        WaitForElementNotVisible(SimpleModalCloseButton);
        FormModal.Click();
        WaitForTextToBe(FormModalText, "Modal Containing A Form");
        return FormModalText.Text;
    }

    public void FillAndSubmitFormModal()
    {
        FormModalNameInput.SendKeys("name");
        FormModalEmailInput.SendKeys("email");
        FormModalMessageInput.SendKeys("text");
        FormModalSubmitButton.Click();
    }
}