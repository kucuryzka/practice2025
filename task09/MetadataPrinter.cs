using System.Reflection;
using System;
using System.IO;

namespace task09;


public class MetadataPrinter
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            System.Console.WriteLine("path not found");
            return;
        }

        string path = args[0];
        if (!File.Exists(path))
        {
            System.Console.WriteLine("file not found");
            return;
        }

        Assembly assembly = Assembly.LoadFrom(path);

        foreach (Type type in assembly.GetTypes())
        {
            System.Console.WriteLine($"class: {type.FullName}");

            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);
            foreach (MethodInfo method in methods)
            {
                System.Console.WriteLine($"method: {method.Name}");

                foreach (var param in method.GetParameters())
                {
                    System.Console.WriteLine($"parameter: {param.Name}, {param.ParameterType.Name}");
                }
            }

            foreach (var attribute in type.GetCustomAttributes())
            {
                System.Console.WriteLine($"attribute: {attribute.GetType().Name}");
            }

            foreach (var constructor in type.GetConstructors())
            {
                System.Console.WriteLine($"constructor: {constructor.Name}");
                foreach (var param in constructor.GetParameters())
                {
                    System.Console.WriteLine($"parameter: {param.Name}, {param.ParameterType.Name}");
                }
            }

        }
    }

    public static void Method() { }

}
