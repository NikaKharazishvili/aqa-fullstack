using UiTests.Pages;
using static Shared.Utils;

namespace UiTests.Tests.Integration;

/// <summary>Tests the Calendar page.</summary>
[TestFixture]
[Category(INTEGRATION)]
[Category(UI)]
[Parallelizable(ParallelScope.Self)]
public class CalendarsTest : BaseTest
{
    CalendarsPage calendarsPage;

    [OneTimeSetUp]
    public void PageSetup()
    {
        calendarsPage = new();
        calendarsPage.GoToCalendarsPage();
    }

    [Test]
    public void VerifyHeaderText() => Assert.That(calendarsPage.GetHeaderText(), Is.EqualTo("Calendars"));

    [Test]
    public void VerifyDateSelection()
    {
        int year = 2000, month = 12, day = 12;
        calendarsPage.SetDate(year, month, day);
        Assert.That(calendarsPage.GetDate(), Is.EqualTo(year + "-" + month + "-" + day));
    }
}