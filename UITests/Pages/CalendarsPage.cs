using OpenQA.Selenium;

namespace UiTests.Pages;

/// <summary>Page Object for the Calendars page. Provides methods to navigate the page and select date.</summary>
public class CalendarsPage : BasePage
{
    IWebElement CalendarLink => Find("a[href*='calendars/']");
    IWebElement DatePicker => Find("#g1065-1-selectorenteradate");

    public void GoToCalendarsPage() => HoverAndClick(CalendarLink);

    public void SetDate(int year, int month, int day)
    {
        // Format the date correctly (YYYY-MM-DD)
        String date = year + "-" + month + "-" + day;

        // Set the date value using JavaScript and trigger an input event
        ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].value=arguments[1]; arguments[0].dispatchEvent(new Event('input'))", DatePicker, date);
    }

    public String? GetDate() => DatePicker.GetDomProperty("value");
}