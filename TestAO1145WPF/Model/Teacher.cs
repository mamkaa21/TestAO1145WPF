using System;
using System.Collections.Generic;

namespace TestAO1145WPF.Model;

public partial class Teacher
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Subject> subject { get; set; } = new List<Subject>();
}
