using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace TestAO1145WPF.Model;

public partial class Student
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Age { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? IdClass { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();

    public static explicit operator StModel(Student student)
    {
        return new StModel
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Age = student.Age,
            Login = student.Login,
            Password = student.Password,
            IdClass = student.IdClass,
            Classes = student.Classes,
            Tests = student.Tests

        };
    }
}



public partial class StModel
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Age { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? IdClass { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}

