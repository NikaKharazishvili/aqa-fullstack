using UiTests.Pages;
using static Shared.Utils;

namespace UiTests.Tests.Integration;

/// <summary>Tests the Popups page.</summary>
[TestFixture]
[Category(INTEGRATION)]
[Category(UI)]
[Parallelizable(ParallelScope.Self)]
public class PopupsTest : BaseTest
{
    PopupsPage popupsPage;

    [OneTimeSetUp]
    public void PageSetup()
    {
        popupsPage = new();
        popupsPage.GoToPopupsPage();
    }

    [Test]
    public void VerifyHeaderText() => Assert.That(popupsPage.GetHeaderText(), Is.EqualTo("Popups"));

    [Test]
    public void TestAlertPopup()
    {
        popupsPage.ClickAlertPopup();
        Assert.That(popupsPage.GetAlert().Text, Is.EqualTo("Hi there, pal!"));
        popupsPage.GetAlert().Accept();
    }

    [Test]
    public void TestConfirmPopup()
    {
        // Accept scenario
        popupsPage.ClickConfirmPopup();
        Assert.That(popupsPage.GetAlert().Text, Is.EqualTo("OK or Cancel, which will it be?"));
        popupsPage.GetAlert().Accept();
        Assert.That(popupsPage.GetConfirmPopupText(), Is.EqualTo("OK it is!"));

        // Dismiss scenario
        popupsPage.ClickConfirmPopup();
        Assert.That(popupsPage.GetAlert().Text, Is.EqualTo("OK or Cancel, which will it be?"));
        popupsPage.GetAlert().Dismiss();
        Assert.That(popupsPage.GetConfirmPopupText(), Is.EqualTo("Cancel it is!"));
    }

    [Test]
    public void TestPromptPopup()
    {
        // Accept with input
        popupsPage.ClickPromptPopup();
        Assert.That(popupsPage.GetAlert().Text, Is.EqualTo("Hi there, what's your name?"));
        popupsPage.GetAlert().SendKeys("Aham Brahmasmi");
        popupsPage.GetAlert().Accept();
        Assert.That(popupsPage.GetPromptPopupText(), Is.EqualTo("Nice to meet you, Aham Brahmasmi!"));

        // Dismiss without input
        popupsPage.ClickPromptPopup();
        Assert.That(popupsPage.GetAlert().Text, Is.EqualTo("Hi there, what's your name?"));
        popupsPage.GetAlert().Dismiss();
        Assert.That(popupsPage.GetPromptPopupText(), Is.EqualTo("Fine, be that way..."));
    }
}
