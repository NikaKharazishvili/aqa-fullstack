using OpenQA.Selenium;

namespace UiTests.Pages;

/// <summary>Page Object for the Sliders page. Provides methods to navigate the page and move slider.</summary>
public class SliderPage : BasePage
{
    IWebElement SliderLink => Find("a[href*='slider']");
    IWebElement Slider => Find("#slideMe");

    public void GoToSliderPage() => HoverAndClick(SliderLink);

    public void MoveSlider(int targetValue) => ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].value=arguments[1]; arguments[0].dispatchEvent(new Event('input'))", Slider, targetValue);

    public int GetSliderValue() => int.TryParse(Slider.GetDomProperty("value"), out int result) ? result : 0;
}