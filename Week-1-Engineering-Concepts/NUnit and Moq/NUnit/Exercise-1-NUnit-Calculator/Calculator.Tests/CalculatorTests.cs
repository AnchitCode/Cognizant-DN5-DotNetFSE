using CalcLibrary;
using NUnit.Framework;

namespace Calculator.Tests;

[TestFixture]
public class CalculatorTests
{
    private Calculator _calculator;

    [SetUp]
    public void Setup()
    {
        _calculator = new Calculator();
    }

    [TearDown]
    public void TearDown()
    {
        _calculator = null;
    }

    [TestCase(2, 3, 5)]
    [TestCase(10, 20, 30)]
    [TestCase(-5, 5, 0)]
    [TestCase(100, 200, 300)]
    public void Add_ShouldReturnExpectedResult(int a, int b, int expected)
    {
        int actual = _calculator.Add(a, b);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Ignore("Sample ignored test")]
    [Test]
    public void IgnoreTest()
    {
        Assert.Pass();
    }
}
