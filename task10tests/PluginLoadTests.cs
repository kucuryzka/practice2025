using Xunit;
using System.IO;
using System.Reflection;
using task10;


namespace task10tests;

public class PluginLoadTests
{

    [Fact]
    public void NonExistentDirectory_Test()
    {
        var string_writer = new StringWriter();
        Console.SetOut(string_writer);

        var loader = new PluginLoader(@"\some\directory");
        loader.FindPlugins();

        var output = string_writer.ToString();
        Assert.Contains("directory not found", output);

    }

    [Fact]
    public void PluginLoaderLoadsSamplePlugin_Test()
    {
        var string_writer = new StringWriter();
        Console.SetOut(string_writer);

        var test_path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData");

        var loader = new PluginLoader(test_path);
        loader.FindPlugins();

        var output = string_writer.ToString();
        Assert.Contains("sample plugin", output);
    }

    [Fact]
    public void PluginLoaderProcessesDependenciesCorrect_Test()
    {
        var string_writer = new StringWriter();
        Console.SetOut(string_writer);

        var test_path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData");

        var loader = new PluginLoader(test_path);
        loader.FindPlugins();

        var output = string_writer.ToString().Split(Environment.NewLine);

        Assert.Equal("sample plugin", output[0]);
        Assert.Equal("dependant plugin", output[1]);

    }
}
