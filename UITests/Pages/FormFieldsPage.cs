using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UiTests.Pages;

public class FormFieldsPage : BasePage
{
    private IWebElement FormFieldsLink => Find("a[href*='form-fields']");
    private IWebElement NameInput => Find("#name-input");
    private IWebElement PasswordInput => Find("input[type='password']");
    private List<IWebElement> FavouriteDrinks => FindMany("[name='fav_drink']");
    private List<IWebElement> FavouriteColors => FindMany("[name='fav_color']");
    private IWebElement AutomationQuestionDropdown => Find("#automation");
    private IWebElement EmailInput => Find("#email");
    private IWebElement MessageInput => Find("#message");
    private IWebElement SubmitButton => Find("#submit-btn");

    public void GoToFormFieldsPage() => FormFieldsLink.Click();

    public FormFieldsPage FillForm(string name, string password, string favDrink, int favColorIndex, string doYouLikeAutomation, string email, string message)
    {
        // Fill name and password
        NameInput.SendKeys(name);
        PasswordInput.SendKeys(password);

        // Select favourite drink by value
        var selectedFavDrink = FavouriteDrinks.FirstOrDefault(d => d.GetAttribute("value") == favDrink);
        if (selectedFavDrink == null) throw new Exception($"Drink not found: {favDrink}");
        HoverAndClick(selectedFavDrink);

        // Select favourite color by index (0-4)
        if (favColorIndex < 0 || favColorIndex >= FavouriteColors.Count) throw new ArgumentOutOfRangeException(nameof(favColorIndex), "Invalid color index");
        HoverAndClick(FavouriteColors[favColorIndex]);

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