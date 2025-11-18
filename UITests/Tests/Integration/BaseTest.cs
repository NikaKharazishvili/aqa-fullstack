using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Text.Json;
using UiTests.Core;
using static Shared.Utils;

namespace UiTests.Tests;

[TestFixture]
[Category(INTEGRATION)]
[Category(UI)]
[Parallelizable(ParallelScope.All)]
public abstract class BaseTest
{
    [OneTimeSetUp]
    public void Setup()
    {
        
    }
}