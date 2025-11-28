using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UiTests.Pages;

/// <summary>Page Object for the FormFields page. Provides methods to navigate the page and fill forms.</summary>
public class FormFieldsPage : BasePage
{
    IWebElement FormFieldsLink => Find("a[href*='form-fields']");
    IWebElement NameInput => Find("#name-input");
    IWebElement PasswordInput => Find("input[type='password']");
    List<IWebElement> FavouriteDrinks => FindMany("[name='fav_drink']");
    List<IWebElement> FavouriteColors => FindMany("[name='fav_color']");
    IWebElement AutomationQuestionDropdown => Find("#automation");
    IWebElement EmailInput => Find("#email");
    IWebElement MessageInput => Find("#message");
    IWebElement SubmitButton => Find("#submit-btn");

    public void GoToFormFieldsPage() => HoverAndClick(FormFieldsLink);

    public FormFieldsPage FillForm(string name, string password, string favDrink, int favColorIndex, string doYouLikeAutomation, string email, string message)
    {
        // Fill name and password
        NameInput.SendKeys(name);
        PasswordInput.SendKeys(password);

        // Select favourite drink by value
        var selectedFavDrink = FavouriteDrinks.FirstOrDefault(d => d.GetAttribute("value") == favDrink);
        if (selectedFavDrink == null) throw new Exception($"Drink not found: {favDrink}");
        selectedFavDrink.Click();;

        // Select favourite color by index (0-4)
        if (favColorIndex < 0 || favColorIndex >= FavouriteColors.Count) throw new ArgumentOutOfRangeException(nameof(favColorIndex), "Invalid color index");
        FavouriteColors[favColorIndex].Click();

        // Dropdown by visible text
        new SelectElement(AutomationQuestionDropdown).SelectByText(doYouLikeAutomation);

        // Fill email and message
        EmailInput.SendKeys(email);
        MessageInput.SendKeys(message);

        return this;
    }

    public FormFieldsPage HoverAndClickSubmit()
    {
        HoverAndClick(SubmitButton);
        return this;
    }
}