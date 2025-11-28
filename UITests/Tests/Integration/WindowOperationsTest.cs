using UiTests.Pages;
using static Shared.Utils;

namespace UiTests.Tests.Integration;

/// <summary>Tests the Window Operations page.</summary>
[TestFixture]
[Category(INTEGRATION)]
[Category(UI)]
[Parallelizable(ParallelScope.Self)]
public class WindowOperationsTest : BaseTest
{
    WindowOperationsPage windowOperationsPage;
    readonly string expectedUrl = "https://automatenow.io/";

    [OneTimeSetUp]
    public void PageSetup()
    {
        windowOperationsPage = new();
        windowOperationsPage.GoToWindowOperationsPage();
    }

    [Test]
    public void VerifyHeaderText() => 
        Assert.That(windowOperationsPage.GetHeaderText(), Is.EqualTo("Window Operations"));

    [Test]
    public void VerifyNewTabOpensAndCloses()
    {
        string newTabUrl = windowOperationsPage.OpenNewTabAndGetUrl();
        Assert.That(newTabUrl, Is.EqualTo(expectedUrl));
    }

    [Test]
    public void VerifyWindowIsReplaced()
    {
        string newTabUrl = windowOperationsPage.ReplaceWindowAndGetUrl();
        Assert.That(newTabUrl, Is.EqualTo(expectedUrl));
    }

    [Test]
    public void VerifyNewWindowOpensAndCloses()
    {
        string newTabUrl = windowOperationsPage.OpenNewWindowAndGetUrl();
        Assert.That(newTabUrl, Is.EqualTo(expectedUrl));
    }
}