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

    // Addition
    [TestCase(2, 3, 5)]
    [TestCase(10, 20, 30)]
    public void Add_Test(int a, int b, int expected)
    {
        Assert.AreEqual(expected, _calculator.Add(a, b));
    }

    // Subtraction
    [TestCase(10, 5, 5)]
    [TestCase(50, 20, 30)]
    [TestCase(-5, -5, 0)]
    public void Subtract_Test(int a, int b, int expected)
    {
        Assert.AreEqual(expected, _calculator.Subtract(a, b));
    }

    // Multiplication
    [TestCase(2, 3, 6)]
    [TestCase(5, 5, 25)]
    [TestCase(-2, 4, -8)]
    public void Multiply_Test(int a, int b, int expected)
    {
        Assert.AreEqual(expected, _calculator.Multiply(a, b));
    }

    // Division
    [TestCase(10, 2, 5)]
    [TestCase(20, 5, 4)]
    public void Divide_Test(int a, int b, int expected)
    {
        Assert.AreEqual(expected, _calculator.Divide(a, b));
    }

    [Test]
    public void Divide_By_Zero_Test()
    {
        try
        {
            _calculator.Divide(10, 0);
            Assert.Fail("Division by zero");
        }
        catch (ArgumentException ex)
        {
            Assert.AreEqual("Division by zero", ex.Message);
        }
    }

    // Void Method
    [Test]
    public void TestAddAndClear()
    {
        _calculator.Add(10, 20);

        Assert.AreEqual(30, _calculator.GetResult);

        _calculator.AllClear();

        Assert.AreEqual(0, _calculator.GetResult);
    }
}
