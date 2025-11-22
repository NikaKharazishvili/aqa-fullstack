using UiTests.Pages;
using UiTests.Core;
using static Shared.Utils;

namespace UiTests.Tests;

[TestFixture]
[Category(INTEGRATION)]
[Category(UI)]
[Parallelizable(ParallelScope.Children)]
public class FormFieldsTest : BaseTest
{
    FormFieldsPage formFieldsPage;

    [OneTimeSetUp]
    public void PageSetup()
    {
        formFieldsPage = new FormFieldsPage();
        formFieldsPage.GoToFormFieldsPage();
    }

    [Test]
    public void VerifyHeaderText() => Assert.That(formFieldsPage.GetHeaderText(), Is.EqualTo("Form Fields"));

    [Test]
    public void FillForms()
    {
        formFieldsPage
        .FillForm(
            ConfigReader.Get<string>("Name"),
            ConfigReader.Get<string>("Password"),
            ConfigReader.Get<string>("FavDrink"),
            ConfigReader.Get<int>("FavColorIndex"),
            ConfigReader.Get<string>("DoYouLikeAutomation"),
            ConfigReader.Get<string>("Email"),
            ConfigReader.Get<string>("Message"))
        .HoverAndClickSubmit()
        .AcceptAlert();
    }
}