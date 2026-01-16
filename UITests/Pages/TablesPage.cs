using OpenQA.Selenium;

namespace UiTests.Pages;

/// <summary>Page Object for the Tables page. Provides methods to navigate the page and retrieve items and their prices.</summary>
public class TablesPage : BasePage
{
    IWebElement TablesLink => Find("a[href*='tables/']");
    List<IWebElement> Items => FindMany(".wp-block-table table tbody tr td:first-child");
    List<IWebElement> Prices => FindMany(".wp-block-table table tbody tr td:last-child");

    public void GoToTablesPage() => HoverAndClick(TablesLink);

    public Dictionary<string, string> GetDisplayedItemsAndPrices()
    {
        var displayedMap = new Dictionary<string, string>();

        for (int i = 1; i < Items.Count; i++) // Skip header row
        {
            string item = Items[i].Text.Trim();
            string price = Prices[i].Text.Trim();
            displayedMap[item] = price;
        }

        return displayedMap;
    }
}