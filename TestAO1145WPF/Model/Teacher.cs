using System;
using System.Collections.Generic;

namespace TestAO1145WPF;

public partial class Teacher
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? IdClass { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual Class? IdClassNavigation { get; set; }

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
