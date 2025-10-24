global using static Shared.Utils;

namespace APITests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Print("სს");
        Assert.Pass();
    }
}
