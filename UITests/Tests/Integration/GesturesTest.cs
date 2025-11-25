using UiTests.Pages;
using static Shared.Utils;

namespace UiTests.Tests;

/// <summary>Tests the Gestures page.</summary>
[TestFixture]
[Category(INTEGRATION)]
[Category(UI)]
[Parallelizable(ParallelScope.Self)]
public class GesturesTest : BaseTest
{
    GesturesPage gesturesPage;

    [OneTimeSetUp]
    public void PageSetup()
    {
        gesturesPage = new GesturesPage();
        gesturesPage.GoToGesturesPage();
    }

    [Test]
    public void VerifyHeaderText() => Assert.That(gesturesPage.GetHeaderText(), Is.EqualTo("Gestures"));

    [Test]
    public void PerformGesturesActions() => gesturesPage.MoveBox().DragAndDropImage();
}