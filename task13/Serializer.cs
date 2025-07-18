﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace task13;

public class Subject
{
    public string Name { get; set; }
    public int Grade { get; set; }
}


public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [JsonConverter(typeof(DateTimeCustomConverter))]
    public DateTime BirthDate { get; set; }
    public List<Subject> Grades { get; set; }
}


public class DateTimeCustomConverter : JsonConverter<DateTime>
{
    private string required_format = "dd.MM.yyyy";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string date = reader.GetString();

        return DateTime.ParseExact(date, required_format, null);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        string date = value.ToString(required_format);

        writer.WriteStringValue(date);
    }
}


public static class Serializer
{
    static JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true,
        Converters = {new DateTimeCustomConverter() }
    };

    public static string Serialize(Student student)
    {
        return JsonSerializer.Serialize(student, serializerOptions);
    }

    public static Student Deserialize(string json)
    {
        return JsonSerializer.Deserialize<Student>(json, serializerOptions);
    }

    public static void Save(Student student, string path)
    {
        File.WriteAllText(path, Serialize(student));
    }

    public static Student Load(string path)
    {
        return Deserialize(File.ReadAllText(path));
    }
}