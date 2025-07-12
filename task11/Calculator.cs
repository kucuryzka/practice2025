using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.IO;
using System.Reflection;

namespace task11;

public interface ICalculator
{
    int Add(int a, int b);
    int Minus(int a, int b);
    int Mul(int a, int b);
    int Div(int a, int b);
}

public class CalculatorGenerator
{
    public static ICalculator GenerateCalculator(string calc_code)
    {
        SyntaxTree calcTree = CSharpSyntaxTree.ParseText(calc_code);

        var required_libs = new MetadataReference[]
        { MetadataReference.CreateFromFile(typeof(object).Assembly.Location), MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location)};

        var compilated = CSharpCompilation.Create("CalculatorAssembly", new[] { calcTree }, required_libs, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var memoryStream = new MemoryStream();
        var compCalc = compilated.Emit(memoryStream);

        if (!compCalc.Success)
        {
            throw new Exception("compilation error");
        }

        memoryStream.Seek(0, SeekOrigin.Begin);

        var calcAssembly = Assembly.Load(memoryStream.ToArray());
        var calcType = calcAssembly.GetType("Calculator");
        return (ICalculator)Activator.CreateInstance(calcType!)!;
    }
}
