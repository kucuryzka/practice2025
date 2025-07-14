using Xunit;
using task13;

namespace task13tests;

public class SerializerTests
{
    [Fact]
    public void IgnoreNull_Test()
    {
        var student = new Student
        {
            FirstName = "Ivan",
            LastName = "Ivanov",
            BirthDate = DateTime.Parse("16.08.2006"),
            Grades = null
        };

        var serialized = Serializer.Serialize(student);

        Assert.Contains("FirstName", serialized);
        Assert.Contains("LastName", serialized);
        Assert.Contains("BirthDate", serialized);

        Assert.DoesNotContain("Grades", serialized);
    }

    [Fact]
    public void CustomConverter_Test()
    {
        var student = new Student
        {
            FirstName = "Ivan",
            LastName = "Ivanov",
            BirthDate = DateTime.Parse("16.08.2006"),
            Grades = null
        };

        var serialized = Serializer.Serialize(student);


        Assert.Contains("16.08.2006", serialized);
    }

    [Fact]
    public void Deserialization_Test()
    {
        var student = new Student
        {
            FirstName = "Ivan",
            LastName = "Ivanov",
            BirthDate = DateTime.Parse("16.08.2006"),
            Grades = new List<Subject> { new Subject { Name = "Math", Grade = 5 } }
        };

        var serialized = Serializer.Serialize(student);
        var deserialized = Serializer.Deserialize(serialized);

        Assert.Equal(student.FirstName, deserialized.FirstName);
        Assert.Equal(student.LastName, deserialized.LastName);
        Assert.Equal(student.BirthDate, deserialized.BirthDate);
        Assert.Equivalent(student.Grades, deserialized.Grades);
    }

    [Fact]
    public void SaveLoad_Test()
    {
        var student = new Student
        {
            FirstName = "Ivan",
            LastName = "Ivanov",
            BirthDate = DateTime.Parse("16.08.2006"),
            Grades = new List<Subject> { new Subject { Name = "Math", Grade = 5 } }
        };

        Serializer.Save(student, "test.json");
        var saved_student = Serializer.Load("test.json");

        Assert.Equal(student.FirstName, saved_student.FirstName);
        Assert.Equal(student.LastName, saved_student.LastName);
        Assert.Equal(student.BirthDate, saved_student.BirthDate);
        Assert.Equivalent(student.Grades, saved_student.Grades);

        File.Delete("test.json");
    }
}