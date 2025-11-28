using UiTests.Pages;
using static Shared.Utils;

namespace UiTests.Tests.Integration;

/// <summary>Tests the Modals page.</summary>
[TestFixture]
[Category(INTEGRATION)]
[Category(UI)]
[Parallelizable(ParallelScope.Self)]
public class ModalsTest : BaseTest
{
    ModalsPage modalsPage;

    [OneTimeSetUp]
    public void PageSetup()
    {
        modalsPage = new();
        modalsPage.GoToModalsPage();
    }

    [Test]
    public void VerifyHeaderText() => Assert.That(modalsPage.GetHeaderText(), Is.EqualTo("Modals"));

    [Test]
    public void VerifyModalWorkingProperly()
    {
        // Verify simple modal
        Assert.That(modalsPage.OpenSimpleModalAndGetTextThenClose(), Is.EqualTo("Simple Modal"));

        // Verify form modal
        Assert.That(modalsPage.OpenFormModalAndGetText(), Is.EqualTo("Modal Containing A Form"));
        modalsPage.FillAndSubmitFormModal();
    }
}