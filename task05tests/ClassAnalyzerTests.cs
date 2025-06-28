using Xunit;
using Moq;
using task05;

namespace task05tests;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class MyTestAttribute : Attribute { }

[MyTest]
public class ClassWithAttribute { }


public class TestClass
{
    public int PublicField;
    private string _privateField;
    public int Property { get; set; }

    public void Method() { }
    public void MethodWithParams(int param1, int param2){}
}

[Serializable]
public class AttributedClass { }

public class ClassAnalyzerTests
{
    [Fact]
    public void GetPublicMethods_ReturnsCorrectMethods()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methods = analyzer.GetPublicMethods();

        Assert.Contains("Method", methods);
    }

    [Fact]
    public void GetAllFields_IncludesPrivateFields()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var fields = analyzer.GetAllFields();

        Assert.Contains("_privateField", fields);
    }

    [Fact]
    public void GetProperties_ReturnsCorrectProperties()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var properties = analyzer.GetProperties();

        Assert.Contains("Property", properties);
    }

    [Fact]
    public void HasAttribute_ReturnsCorrectValue()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var analyzer2 = new ClassAnalyzer(typeof(ClassWithAttribute));

        Assert.False(analyzer.HasAttribute<MyTestAttribute>());
        Assert.True(analyzer2.HasAttribute<MyTestAttribute>());
    }

    [Fact]
    public void GetMethodParams_ReturnsCorrectParams()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));

        var param = analyzer.GetMethodParams("MethodWithParams");
        var no_param = analyzer.GetMethodParams("Method");

        Assert.Contains("Void", param);
        Assert.Contains("param1", param);
        Assert.Contains("param2", param);

        Assert.True(no_param.Count() == 1); // only return type

    }
}