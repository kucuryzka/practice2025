using System;
using System.IO;
using System.Reflection;

namespace task10;


public interface IPlugin
{
    public void Execute();
}


[AttributeUsage(AttributeTargets.Class)]
public class PluginLoadAttribute : Attribute
{
    public string[] dependencies = Array.Empty<string>();

    public PluginLoadAttribute(params string[] dependencies)
    {
        this.dependencies = dependencies;
    }
}


public class PluginLoader
{
    private string _path;
    private List<Assembly> _assemblies = new List<Assembly>();
    private List<(Type type, string[] deps)> _plugins = new List<(Type type, string[] deps)>();
    private List<string> _loaded = new List<string>();

    public PluginLoader(string path)
    {
        _path = path;
    }

    public void FindPlugins()
    {
        if (!Directory.Exists(_path))
        {
            System.Console.WriteLine("directory not found");
            return;
        }
        
        var dll_paths = Directory.GetFiles(_path, "*.dll");

        foreach (var dll_path in dll_paths)
        {
            var assembly = Assembly.LoadFrom(dll_path);
            _assemblies.Add(assembly);
        }

        foreach (var assembly in _assemblies)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<PluginLoadAttribute>() != null)
                {
                    var attribute = type.GetCustomAttribute<PluginLoadAttribute>();
                    _plugins.Add((type, attribute.dependencies));
                }
            }
        }


        while (_loaded.Count != _plugins.Count)
        {
            foreach (var plugin in _plugins)
            {
                if (_loaded.Contains(plugin.type.Name)) continue;

                if (plugin.deps.All(dep => _loaded.Contains(dep)))
                {
                    var instance = (IPlugin)Activator.CreateInstance(plugin.type);
                    instance.Execute();

                    _loaded.Add(plugin.type.Name);
                }


            }
        }


    }

}

