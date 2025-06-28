using System;
using System.Reflection;
using System.Collections.Generic;

namespace task05;

public class ClassAnalyzer
{
    private Type _type;

    public ClassAnalyzer(Type type)
    {
        _type = type;
    }

    public IEnumerable<string> GetPublicMethods()
    {
        return _type.GetMethods(BindingFlags.Public | BindingFlags.Instance).Select(el => el.Name).ToList();
    }

    public IEnumerable<string> GetMethodParams(string methodname)
    {
        List<string> arr = new List<string>();

        var method = _type.GetMethods().Where(el => el.Name == methodname).First();
        arr.Add(method.ReturnType.Name);

        
        foreach (var el in method.GetParameters())
        {
            arr.Add(el.Name);
        }

        return arr;

    }

    public IEnumerable<string> GetAllFields()
    {
        return _type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Select(fields => fields.Name);
    }

    public IEnumerable<string> GetProperties()
    {
        return _type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Select(prop => prop.Name);
    }

    public bool HasAttribute<T>() where T : Attribute
    {
        return _type.GetCustomAttributes(typeof(T), false).Count() > 0;
    }
}