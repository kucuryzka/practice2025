using System;
using System.IO;
using System.Reflection;
using Xunit;
using task09;

namespace MetadataPrinterTests;


public class MetadataPrinterTests
{
    [Fact]
    public void NoArgumentTest()
    {
        StringWriter stringWriter = new StringWriter();
        TextWriter textWriter = Console.Out;
        Console.SetOut(stringWriter);

        MetadataPrinter.Main(Array.Empty<string>());
        var output = stringWriter.ToString();
        Assert.Contains("path not found", output);


        Console.SetOut(textWriter);
        stringWriter.Dispose();
    }

    [Fact]
    public void NonExistingFileTest()
    {
        StringWriter stringWriter = new StringWriter();
        TextWriter textWriter = Console.Out;
        Console.SetOut(stringWriter);

        MetadataPrinter.Main(new[] { "somefile.dll" });
        var output = stringWriter.ToString();
        Assert.Contains("file not found", output);


        Console.SetOut(textWriter);
        stringWriter.Dispose();
    }
}