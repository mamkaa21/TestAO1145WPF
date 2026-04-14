using System;
using System.Collections.Generic;

namespace TestAO1145WPF.Model;

public partial class Aswer
{
    public int Id { get; set; }

    public string? RightAnswer { get; set; }

    public string? LieAnswer { get; set; }

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
