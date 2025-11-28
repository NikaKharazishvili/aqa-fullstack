using OpenQA.Selenium;

namespace UiTests.Pages;

/// <summary>
/// Page Object for the Window Operations page.
/// Provides methods to navigate the page and interact with and test the behavior of opening a New Tab, replacing the current window, and opening a New Window.
/// </summary>
public class WindowOperationsPage : BasePage
{
    IWebElement WindowOperationsLink => Find("a[href*='window-operations/']");
    IWebElement NewTab => Find(".custom_btn[onclick='newTab()']");
    IWebElement ReplaceWindow => Find(".custom_btn[onclick='newWindowSelf()']");
    IWebElement NewWindow => Find(".custom_btn[onclick='newWindow()']");

    public void GoToWindowOperationsPage() => HoverAndClick(WindowOperationsLink);

    // Opens a new tab, captures its URL, closes it, and switches back to the original tab
    public string OpenNewTabAndGetUrl()
    {
        NewTab.Click();
        var windows = Driver.WindowHandles.ToList();
        Driver.SwitchTo().Window(windows[1]);
        string currentUrl = Driver.Url;
        Driver.Close();
        Driver.SwitchTo().Window(windows[0]);
        return currentUrl;
    }

    // Replaces window, captures its URL, navigates back
    public string ReplaceWindowAndGetUrl()
    {
        ReplaceWindow.Click();
        string currentUrl = Driver.Url;
        Driver.Navigate().Back();
        return currentUrl;
    }

    // Opens a completely new browser window, captures its URL, closes it, and switches back to the original window
    public string OpenNewWindowAndGetUrl()
    {
        string originalWindow = Driver.CurrentWindowHandle;
        NewWindow.Click();

        // Wait until a new window appears
        var windowHandles = Driver.WindowHandles;
        while (windowHandles.Count == 1)
        {
            windowHandles = Driver.WindowHandles;
        }

        // Switch to the new window
        foreach (var handle in windowHandles)
        {
            if (handle != originalWindow)
            {
                Driver.SwitchTo().Window(handle);
                break;
            }
        }

        string currentUrl = Driver.Url;
        Driver.Close();
        Driver.SwitchTo().Window(originalWindow);
        return currentUrl;
    }
}