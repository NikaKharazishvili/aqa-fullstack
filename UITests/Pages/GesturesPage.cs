using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace UiTests.Pages;

/// <summary>Page Object for the Gestures page. Provides methods to navigate the page and perform gestures actions.</summary>
public class GesturesPage : BasePage
{
    IWebElement GesturesLink => Find("a[href*='gestures/']");
    IWebElement BoxHeader => Find("#moveMeHeader");
    IWebElement Image => Find("#dragMe");
    IWebElement ImagePlaceToMove => Find("#div2");

    public void GoToGesturesPage() => HoverAndClick(GesturesLink);

    public GesturesPage MoveBox()
    {
        Actions.ClickAndHold(BoxHeader)
               .MoveByOffset(500, 100)
               .Release()
               .Perform();
        return this;
    }

    public GesturesPage DragAndDropImage()  // JS code is more reliable
    {
        ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[1].appendChild(arguments[0]);", Image, ImagePlaceToMove);
        return this;
    }
}