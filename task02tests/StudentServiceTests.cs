﻿using Xunit;
using task02;

namespace task02tests;


public class StudentServiceTests
{
    private List<Student> _testStudents;
    private StudentService _service;

    public StudentServiceTests()
    {
        _testStudents = new List<Student>
        {
            new() { Name = "Иван", Faculty = "ФИТ", Grades = new List<int> { 5, 4, 5 } },
            new() { Name = "Анна", Faculty = "ФИТ", Grades = new List<int> { 3, 4, 3 } },
            new() { Name = "Петр", Faculty = "Экономика", Grades = new List<int> { 5, 5, 5 } },
            new() { Name = "Сергей", Faculty = "РТФ", Grades = new List<int> { 3, 4, 5 } },
            new() { Name = "Ольга", Faculty = "РТФ", Grades = new List<int> { 3, 3, 3 } },
            new() { Name = "Мария", Faculty = "НХИ", Grades = new List<int> { 4, 5, 5 } }
        };
        _service = new StudentService(_testStudents);
    }

    [Fact]
    public void GetStudentsByFaculty_ReturnsCorrectStudents()
    {
        var result = _service.GetStudentsByFaculty("ФИТ").ToList();
        Assert.Equal(2, result.Count);
        Assert.True(result.All(s => s.Faculty == "ФИТ"));
    }

    [Fact]
    public void GetFacultyWithHighestAverageGrade_ReturnsCorrectFaculty()
    {
        var result = _service.GetFacultyWithHighestAverageGrade();
        Assert.Equal("Экономика", result);
    }

    [Fact]
    public void GroupStudentsByFaculty_ReturnsCorrectGroups()
    {
        var result = _service.GroupStudentsByFaculty();

        Assert.Equal(4, result.Count);
        Assert.Equal(2, result["ФИТ"].Count());
        Assert.Equal(2, result["РТФ"].Count());
        Assert.Equal(1, result["НХИ"].Count());
        Assert.Equal(1, result["Экономика"].Count());
    }

    [Fact]
    public void GetStudentsOrderedByName_ReturnsCorrectOrder()
    {
        var result = _service.GetStudentsOrderedByName().Select(s => s.Name).ToList();

        Assert.Equal(new[] { "Анна", "Иван", "Мария", "Ольга", "Петр", "Сергей" }, result);
    }

    [Fact]
    public void GetStudentsWithMinAverageGrade_ReturnsCorrectStudents()
    {
        var result = _service.GetStudentsWithMinAverageGrade(4.1).ToList();

        Assert.Equal(3, result.Count);
        Assert.True(result.All(student => student.Grades.Average() >= 4.1));
    }
}