using UiTests.Pages;
using static Shared.Utils;

namespace UiTests.Tests.Integration;

/// <summary>Tests the Hover page.</summary>
[TestFixture]
[Category(INTEGRATION)]
[Category(UI)]
[Parallelizable(ParallelScope.Self)]
public class HoverTest : BaseTest
{
    HoverPage hoverPage;

    [OneTimeSetUp]
    public void PageSetup()
    {
        hoverPage = new();
        hoverPage.GoToHoverPage();
    }

    [Test]
    public void VerifyHeaderText() => Assert.That(hoverPage.GetHeaderText(), Is.EqualTo("Hover"));

    [Test]
    public void VerifyHoverWorkingProperly()
    {
        Assert.Multiple(() =>
        {
            Assert.That(hoverPage.GetHoverText(), Is.EqualTo("Mouse over me"));
            hoverPage.HoverOverElement();
            Assert.That(hoverPage.GetHoverText(), Is.EqualTo("You did it!"));
        });
    }
}