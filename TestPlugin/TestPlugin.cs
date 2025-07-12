using task10;

namespace TestPlugin;

[PluginLoad]
public class SamplePlugin : IPlugin
{
    public void Execute()
    {
        System.Console.WriteLine("sample plugin");
    }
}

[PluginLoad("SamplePlugin")]
public class DependantPlugin : IPlugin
{
    public void Execute()
    {
        System.Console.WriteLine("dependant plugin");
    }
}
