using Xunit;
using task11;

namespace task11tests;

public class CalculatorTests
{
    public string calculatorClass = @"
    using task11;
    public class Calculator : ICalculator
    {
    public int Add(int a, int b) => a + b;
    public int Minus(int a, int b) => a - b;
    public int Mul(int a, int b) => a * b;
    public int Div(int a, int b) => a / b;
    }
    ";
    [Fact]
    public void AddReturnsCorrectAnswer_Test()
    {
        var calculator = CalculatorGenerator.GenerateCalculator(calculatorClass);
        Assert.Equal(5, calculator.Add(2, 3));
    }

    [Fact]
    public void MinusReturnsCorrectAnswer_Test()
    {
        var calculator = CalculatorGenerator.GenerateCalculator(calculatorClass);
        Assert.Equal(5, calculator.Minus(7, 2));
    }

    [Fact]
    public void MulReturnsCorrectAnswer_Test()
    {
        var calculator = CalculatorGenerator.GenerateCalculator(calculatorClass);
        Assert.Equal(6, calculator.Mul(2, 3));
    }

    [Fact]
    public void DivReturnsCorrectAnswer_Test()
    {
        var calculator = CalculatorGenerator.GenerateCalculator(calculatorClass);
        Assert.Equal(5, calculator.Div(25, 5));
    }

    [Fact]
    public void DivByZeroThrowsException_Test()
    {
        var calculator = CalculatorGenerator.GenerateCalculator(calculatorClass);
        Assert.Throws<DivideByZeroException>(() => calculator.Div(2, 0));
    }
}