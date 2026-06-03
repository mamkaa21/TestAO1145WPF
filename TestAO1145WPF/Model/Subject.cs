using System;
using System.Collections.Generic;

namespace TestAO1145WPF.Model;

public partial class Subject
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
