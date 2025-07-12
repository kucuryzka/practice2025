using System;
using System.Reflection;

namespace task07;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class DisplayNameAttribute : Attribute
{
    public string DisplayName { get; }
    public DisplayNameAttribute(string name) => DisplayName = name;
}


[AttributeUsage(AttributeTargets.Class)]
public class VersionAttribute : System.Attribute
{
    public int Major { get; }
    public int Minor { get; }

    public VersionAttribute(string version)
    {
        string[] arr = version.Split('.');

        Major = int.Parse(arr[0]);
        Minor = int.Parse(arr[1]);
    }
}


[DisplayName("Пример класса")]
[Version("1.0")]
public class SampleClass
{
    [DisplayName("Числовое свойство")]
    public int Number { get; }


    [DisplayName("Тестовый метод")]
    public void TestMethod()
    {

    }

}


public class ReflectionHelper
{
    private Type _type;



    public ReflectionHelper(Type type) => _type = type;

    public void PrintTypeInfo()
    {
        if (_type == null) return;

        var attribute = _type.GetCustomAttribute<DisplayNameAttribute>();
        if (attribute != null) System.Console.WriteLine(attribute.DisplayName);

        var attribute_v = _type.GetCustomAttribute<VersionAttribute>();
        if (attribute_v != null) System.Console.WriteLine($"{attribute_v.Major}.{attribute_v.Minor}");

        var methods = _type.GetMethods(BindingFlags.Public | BindingFlags.Instance).ToList();
        foreach (var method in methods)
        {
            var attribute_m = method.GetCustomAttribute<DisplayNameAttribute>();
            if (attribute_m != null)
            {
                System.Console.WriteLine(attribute_m.DisplayName);
            }
        }


        var properties = _type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).ToList();
        foreach (var property in properties)
        {
            var attribute_p = property.GetCustomAttribute<DisplayNameAttribute>();
            if (attribute_p != null)
            {
                System.Console.WriteLine(attribute_p.DisplayName);
            }
        }


    }
}
