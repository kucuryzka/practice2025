using System;
using System.Collections.Generic;
using System.Linq;


namespace task02;


public class Student
{
    public string Name { get; set; }
    public string Faculty { get; set; }
    public List<int> Grades { get; set; }
}


public class StudentService
{
    private readonly List<Student> _students;

    public StudentService(List<Student> students) => _students = students;


    public IEnumerable<Student> GetStudentsByFaculty(string faculty)
        =>  _students.Where(student => student.Faculty == faculty);



    public IEnumerable<Student> GetStudentsWithMinAverageGrade(double minAverageGrade)
        => _students.Where(student => student.Grades.Count > 0 && student.Grades.Average() >= minAverageGrade);


    public IEnumerable<Student> GetStudentsOrderedByName()
        => _students.OrderBy(student => student.Name);


    public ILookup<string, Student> GroupStudentsByFaculty()
        => _students.ToLookup(student => student.Faculty);


    public string GetFacultyWithHighestAverageGrade()
        => _students.Where(student => student.Grades.Count > 0)
        .GroupBy(student => student.Faculty)
        .Select(el => new { Faculty = el.Key, AverageGrade = el.Average(student => student.Grades.Average()) })
        .OrderByDescending(faculty => faculty.AverageGrade).First().Faculty;
}