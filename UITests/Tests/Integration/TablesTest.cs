using UiTests.Pages;
using static Shared.Utils;

namespace UiTests.Tests.Integration;

/// <summary>Tests the Tables page.</summary>
[TestFixture]
[Category(INTEGRATION)]
[Category(UI)]
[Parallelizable(ParallelScope.Self)]
public class TablesTest : BaseTest
{
    TablesPage tablesPage;

    [OneTimeSetUp]
    public void PageSetup()
    {
        tablesPage = new();
        tablesPage.GoToTablesPage();
    }

    [Test]
    public void VerifyHeaderText() => Assert.That(tablesPage.GetHeaderText(), Is.EqualTo("Tables"));

    [Test]
    public void VerifyItemsPrices()
    {
        var actual = tablesPage.GetDisplayedItemsAndPrices();
        var expected = new Dictionary<string, string>
        {
            { "Oranges", "$3.99" },
            { "Laptop", "$1200.00" },
            { "Marbles", "$1.25" }
        };

        Assert.That(actual, Is.EqualTo(expected), "Items and prices on the table do not match expected values!");
    }
}