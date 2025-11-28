using UiTests.Pages;
using static Shared.Utils;

namespace UiTests.Tests.Integration;

/// <summary>Tests the Slider page.</summary>
[TestFixture]
[Category(INTEGRATION)]
[Category(UI)]
[Parallelizable(ParallelScope.Self)]
public class SliderTest : BaseTest
{
    SliderPage sliderPage;

    [OneTimeSetUp]
    public void PageSetup()
    {
        sliderPage = new();
        sliderPage.GoToSliderPage();
    }

    [Test]
    public void VerifyHeaderText() => Assert.That(sliderPage.GetHeaderText(), Is.EqualTo("Slider"));

    [Test]
    public void MoveSliderBy101()
    {
        sliderPage.MoveSlider(101);
        Assert.That(sliderPage.GetSliderValue(), Is.EqualTo(100));
    }

    [Test]
    public void MoveSliderBy50()
    {
        sliderPage.MoveSlider(50);
        Assert.That(sliderPage.GetSliderValue(), Is.EqualTo(50));
    }

    [Test]
    public void MoveSliderByNegative1()
    {
        sliderPage.MoveSlider(-1);
        Assert.That(sliderPage.GetSliderValue(), Is.EqualTo(0));
    }
}